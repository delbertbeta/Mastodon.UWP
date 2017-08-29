using HtmlAgilityPack;
using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.UWP.ViewModel
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string FaceImage { get; set; }
        public string HeaderImage { get; set; }
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
        private string _note;
        public string Note
        {
            get
            {
                var document = new HtmlDocument();
                document.LoadHtml(_note);
                return document.DocumentNode.InnerText;
            }
            set
            {
                this._note = value;
            }
        }
        public int Posts { get; set; }
        public int Media { get; set; }
        public int Followers { get; set; }
        public int Follows { get; set; }
        public RelationshipModel Relationship { get; set; }
    }
}
