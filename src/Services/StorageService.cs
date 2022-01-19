using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace json_api_test
{
    public interface IStorage
    {
        public Task<string> UploadAsync(byte[] data, string name, string extension = "");
    }

    public class StorageService : IStorage
    {
        private static readonly string _tempStoragePath;
        private readonly Guid _generatedGuid;

        static StorageService()
        {
            _tempStoragePath = Path.Combine(Path.GetTempPath(), "API");
        }

        public StorageService()
        {
            _generatedGuid = Guid.NewGuid();
        }

        public async Task<string> UploadAsync(byte[] data, string name, string extension = "")
        {
            var fileName = GetNewFileName(name, extension);
            await using (var fileStream = new FileStream(fileName, FileMode.CreateNew))
            {
                await fileStream.WriteAsync(data);
            }

            return fileName;
        }
        /*
        public async Task<string> UploadAsync(JToken token, string name)
        {
            var fileName = GetNewFileName();
            await using (var fileStream = new FileStream(fileName, FileMode.CreateNew))
            {
                await using (var requestWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
                {
                    var jsonWriter = new JsonTextWriter(requestWriter);
                    try
                    {
                        await token.WriteToAsync(jsonWriter);
                    }
                    finally
                    {
                        await jsonWriter.CloseAsync();
                    }
                }

            }
            return fileName;
        }
        */

        private string GetNewFileName(string name, string extension)
        {
            var newFolderName = Path.Combine(_tempStoragePath, $"{name}_{_generatedGuid}");
            var newFolder = Directory.CreateDirectory(newFolderName);
            if (!newFolder.Exists)
                newFolder.Create();
            return Path.Combine(newFolderName, $"{Guid.NewGuid()}{extension}");
        }
    }
}
