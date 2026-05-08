using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace EventEase.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private static readonly HashSet<string> AllowedContentTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/webp"
        };

        private readonly IConfiguration _configuration;

        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string?> UploadImageAsync(IFormFile? imageFile, string folderName)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

            if (!AllowedContentTypes.Contains(imageFile.ContentType))
            {
                throw new InvalidOperationException("Only JPG, PNG, GIF, and WEBP image files are allowed.");
            }

            const long maxFileSize = 5 * 1024 * 1024;
            if (imageFile.Length > maxFileSize)
            {
                throw new InvalidOperationException("Image files must be 5 MB or smaller.");
            }

            var connectionString = GetSetting(
                "AzureStorage:ConnectionString",
                "AzureStorage__ConnectionString",
                "APPSETTING_AzureStorage__ConnectionString",
                "CUSTOMCONNSTR_AzureStorage__ConnectionString",
                "AZUREBLOBSTORAGE_AzureStorage__ConnectionString");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Azure Storage is not configured. Set AzureStorage__ConnectionString in App Service App settings before uploading images.");
            }

            var containerName = GetSetting(
                "AzureStorage:ContainerName",
                "AzureStorage__ContainerName",
                "APPSETTING_AzureStorage__ContainerName",
                "CUSTOMCONNSTR_AzureStorage__ContainerName",
                "AZUREBLOBSTORAGE_AzureStorage__ContainerName");

            if (string.IsNullOrWhiteSpace(containerName))
            {
                containerName = "eventease-images";
            }

            var containerClient = new BlobContainerClient(connectionString, containerName);
            await containerClient.CreateIfNotExistsAsync();

            var extension = Path.GetExtension(imageFile.FileName);
            var blobName = $"{folderName.Trim('/')}/{Guid.NewGuid():N}{extension}";
            var blobClient = containerClient.GetBlobClient(blobName);

            await using var stream = imageFile.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = imageFile.ContentType });

            return blobClient.Uri.ToString();
        }

        private string? GetSetting(params string[] keys)
        {
            foreach (var key in keys)
            {
                var value = _configuration[key] ?? Environment.GetEnvironmentVariable(key);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return null;
        }
    }
}
