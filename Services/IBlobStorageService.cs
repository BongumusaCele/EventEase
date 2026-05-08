using Microsoft.AspNetCore.Http;

namespace EventEase.Services
{
    public interface IBlobStorageService
    {
        Task<string?> UploadImageAsync(IFormFile? imageFile, string folderName);
    }
}
