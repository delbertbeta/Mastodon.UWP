using Mastodon.API.Models;
using Mastodon.API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Mastodon.API.Apis
{
    public class Media
    {
        public static async Task<AttachmentModel> UploadMedia(string domain, string accessToken, StorageFile file)
        {
            using (var formData = new MultipartFormDataContent())
            {
                string contentType;
                switch (file.FileType)
                {
                    case ".jpg":
                        contentType = "image/jpeg";
                        break;
                    case ".jpeg":
                        contentType = "image/jpeg";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    default:
                        contentType = "";
                        break;
                }
                formData.Add(new StreamContent(await file.OpenStreamForReadAsync()), "file", file.DisplayName + file.FileType);
                formData.First().Headers.Add("content-type", contentType);
                return await HttpManager.PostAsync<AttachmentModel>($"{domain}{Url.Media}", accessToken, formData);
            }
        }
    }
}
