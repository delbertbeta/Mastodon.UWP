using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using HtmlAgilityPack;

namespace Mastodon.UWP.ViewModel
{
    public class StatusViewModel
    {
        public string FaceImage { get; set; }
        public string Username { get; set; }
        private string _acct;
        public string Acct
        {
            get
            {
                return "@" + _acct;
            }
            set
            {
                _acct = value;
            }
        }
        private string _createAt;
        public string CreateAt
        {
            set
            {
                _createAt = value;
            }
            get
            {
                DateTime time;
                bool state = DateTime.TryParse(_createAt, out time);
                time = time.ToLocalTime();
                DateTime now = DateTime.Now;
                var diff = now.Subtract(time);
                if (diff.TotalSeconds < 60)
                {
                    return Convert.ToInt64(diff.TotalSeconds).ToString() + "s ago";
                }
                else if (diff.TotalSeconds < 3600)
                {
                    return Convert.ToInt64(diff.TotalMinutes).ToString() + "m ago";
                }
                else if (diff.TotalHours < 24)
                {
                    return Convert.ToInt64(diff.TotalHours).ToString() + "h ago";
                }
                else
                {
                    return Convert.ToInt64(diff.TotalDays).ToString() + "d ago";
                }
            }
        }
        private string _content;
        public string Content
        {
            get
            {
                var document = new HtmlDocument();
                document.LoadHtml(_content);
                return document.DocumentNode.InnerText;
            }
            set
            {
                this._content = value;
            }
        }
        public List<API.Models.AttachmentModel> Attachment { get; set; }
    }
}
