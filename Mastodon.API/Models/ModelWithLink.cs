using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Models
{
    public class ModelWithLink<TModel>
    {
        public string PrevUrl { get; set; }
        public string NextUrl { get; set; }
        public TModel Target { get; set; }
    }
}
