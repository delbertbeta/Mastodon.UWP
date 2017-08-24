using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Mastodon.UWP.Pages
{
    public sealed partial class InstancePage : Page
    {
        public InstancePage()
        {
            this.InitializeComponent();
            this.ButtonContent = "Confirm";
            this.ButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 43, 145, 217));
            this.Url = "";
        }

        public string ButtonContent { get; set; }
        public SolidColorBrush ButtonColor { get; set; }
        public string Url { get; set; }

        private void SetErrorState()
        {
            ButtonContent = "Retry";
            ButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 200, 30, 30));
            Bindings.Update();
            ConfirmButton.IsEnabled = true;
        }

        private void SetCheckingState()
        {
            ButtonContent = "Checking...";
            ButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 40, 44, 55));
            ConfirmButton.IsEnabled = false;
            Bindings.Update();
        }

        private void SetRegistingState()
        {
            ButtonContent = "Registing...";
            ButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 40, 44, 55));
            Bindings.Update();
            ConfirmButton.IsEnabled = false;
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            SetCheckingState();
            if (string.IsNullOrWhiteSpace(Url) && Url.IndexOf('.') == -1)
            {
                SetErrorState();
            }
            else
            {
                try
                {
                    var instance = await API.Apis.Instance.GetInstance(Url);
                    SetRegistingState();
                    var app = await API.Apis.Apps.Register(Url, App.AppSetting.AppInfo.ClientName, App.AppSetting.AppInfo.RredirectUris, App.AppSetting.AppInfo.Scopes, App.AppSetting.AppInfo.Website);
                    var url = $"https://{Url}/oauth/authorize?client_id={app.ClientId}&response_type=code&redirect_uri={app.RedirectUri}&scope={string.Join(" ", App.AppSetting.AppInfo.Scopes)}";
                    var result = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, new Uri(url), new Uri(app.RedirectUri));
                    if (result.ResponseStatus != WebAuthenticationStatus.Success) {
                        SetErrorState();
                        return;
                    }
                    var code = Regex.Match(result.ResponseData, "code=(.*)").Groups[1].Value;
                    if (string.IsNullOrEmpty(code))
                    {
                        throw new UnauthorizedAccessException();
                    }
                    var token = await API.Apis.Oauth.GetTokenByCode(Url, app.ClientId, app.ClientSecret, code, app.RedirectUri);
                    var account = await API.Apis.Account.VerifyCredentials(Url, token.AccessToken);
                    App.AppSetting.Accounts.Add(new Model.Settings.Account()
                    {
                        AccountModel = account
                    });
                    App.AppSetting.SelectedAccountIndex = App.AppSetting.Accounts.Count - 1;
                    await App.AppSetting.SaveSetting();
                    Window.Current.Content = new TimeLinePage();
                }
                catch (MastodonException exception)
                {
                    ButtonContent = $"{exception.MastodonError.Error}. Retry";
                    ButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 200, 30, 30));
                    Bindings.Update();
                    ConfirmButton.IsEnabled = true;
                }
                catch
                {
                    SetErrorState();
                }
            }
        }
    }
}
