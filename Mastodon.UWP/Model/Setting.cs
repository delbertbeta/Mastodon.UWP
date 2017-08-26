using Mastodon.UWP.Model.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Mastodon.UWP.Model
{
    public class Setting
    {
        public Setting()
        {
            AppInfo = new ApplicationInfo();
        }

        public ApplicationInfo AppInfo { get; }

        public List<Account> Accounts { get; set; }

        public int SelectedAccountIndex { get; set; }

        public async Task SaveSetting()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var settingFolder = await localFolder.CreateFolderAsync("Settings", CreationCollisionOption.OpenIfExists);
            var file = await settingFolder.CreateFileAsync("Setting.json", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, JsonConvert.SerializeObject(this), Windows.Storage.Streams.UnicodeEncoding.Utf8);
        }

        public static async Task<Setting> GetSetting()
        {
            // Get all the accounts and local settings.
            var localFolder = ApplicationData.Current.LocalFolder;
            var settingFolder = await localFolder.CreateFolderAsync("Settings", CreationCollisionOption.OpenIfExists);
            var settingFile = await settingFolder.TryGetItemAsync("Setting.json") as StorageFile;
            if (settingFile == null)
            {
                var setting = new Setting();
                setting.Accounts = new List<Account>();
                setting.SelectedAccountIndex = 0;
                await setting.SaveSetting();
                return setting;
            }
            else
            {
                return JsonConvert.DeserializeObject<Setting>(await FileIO.ReadTextAsync(settingFile, Windows.Storage.Streams.UnicodeEncoding.Utf8));
            }
        }
    }
}
