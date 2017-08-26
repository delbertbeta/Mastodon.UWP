using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Mastodon.UWP.Services
{
    public enum ResourceType
    {
        Face,
        PreviewImage,
        OriginalImage,
        Avatar,
        Header
    }

    public class WebResourceManager
    {
        private static StorageFolder tempFolder = ApplicationData.Current.TemporaryFolder;

        public static async Task<StorageFile> GetWebResource(string url, ResourceType type)
        {
            var subFolder = await tempFolder.CreateFolderAsync(type.ToString(), CreationCollisionOption.OpenIfExists);
            var fileName = getFileName(url);
            var storageFile = await subFolder.TryGetItemAsync(fileName) as StorageFile;
            if (storageFile != null)
            {
                return storageFile;
            }
            else
            {
                var newFile = await subFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                var fileBuffer = await GetFile(url);
                using (var newFileStream = await newFile.OpenStreamForWriteAsync())
                {
                    await newFileStream.WriteAsync(fileBuffer, 0, fileBuffer.Length);
                }
                return newFile;
            }
        }

        private static string getFileName(string url)
        {
            return url.Substring(url.LastIndexOf('/') + 1);
        }

        private static async Task<byte[]> GetFile(string url)
        {
            using (var httpClient = new HttpClient())
            {
                using (var res = await httpClient.GetAsync(url))
                {
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return await res.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        throw new HttpRequestException("Can't Request such resource.");
                    }
                }
            }
        }
    }

}
