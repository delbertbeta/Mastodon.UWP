using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Models
{
    public class MastodonException: Exception
    {
        public MastodonException(string message, Exception inner, ErrorModel error):base(message, inner)
        {
            MastodonError = error;
        }

        public MastodonException(string message, ErrorModel error): base(message)
        {
            MastodonError = error;
        }

        public ErrorModel MastodonError { get; }
    }
}
