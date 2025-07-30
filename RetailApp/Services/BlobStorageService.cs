using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;

namespace RetailApp.Services
{
    public class BlobStorageService
    {
        private readonly string _connectionString;
        private readonly string _containerName = "product-images";


        public BlobStorageService (string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task UploadFileAsync(string filePath, string fileName)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(fileName);

            using FileStream uploadFileStream = File.OpenRead(filePath);
            await blobClient.UploadAsync(uploadFileStream, overwrite: true);
            uploadFileStream.Close();
        }
    }

}
