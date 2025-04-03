using Microsoft.AspNetCore.Http;

namespace CCP.Service.Integration.BlobStorage
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<List<string>> UploadFilesAsync(List<IFormFile> files);
        Task<List<string>> UploadFilesAsyncWithoutSAS(List<IFormFile> files);
        Task<string> UploadFileAsync(byte[] fileContent, string fileName, string contentType); 
    }
}