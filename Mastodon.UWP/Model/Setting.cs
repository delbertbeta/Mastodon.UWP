using Mastodon.UWP.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.UWP.Model
{
    internal sealed class Setting
    {
        public Setting()
        {
            AppInfo = new ApplicationInfo();
        }

        public ApplicationInfo AppInfo { get; }
    }
}
