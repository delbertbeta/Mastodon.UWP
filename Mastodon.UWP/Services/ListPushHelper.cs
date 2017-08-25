using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.UWP.Services
{
    public class ListPushHelper
    {
        public enum PushMethod
        {
            Head,
            Foot
        }

        static public void PushToList<T>(List<T> source, ref ObservableCollection<T> target, PushMethod method)
        {
            if (method == PushMethod.Head)
            {
                for (int i = source.Count - 1; i >= 0; i--)
                {
                    //if(source[i].Equals(target.First()))
                    //{
                    //    continue;
                    //}
                    //else
                    //{
                        target.Insert(0, source[i]);
                    //}
                }
            }
            if (method == PushMethod.Foot)
            {
                foreach (var item in source)
                {
                    target.Add(item);
                }
                
            }
        }
    }
}
