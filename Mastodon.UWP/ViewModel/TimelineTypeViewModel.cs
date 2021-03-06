﻿using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.UWP.View
{
    public enum TimelineType
    {
        Home,
        Id,
        Tag,
        DataContext,
        Favorites
    }

    public class TimelineTypeViewModel
    {
        public TimelineType TimelineType { get; set; }
        public string TimelineIdentifier { get; set; }
        public List<StatusModel> Source { get; set; }
    }
}
