using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IFileService
{
    Task<string> AddImage(IFormFile file, string path);
    void RemoveImage(string imageName, string path);
    Task<string> GetImageAsBase64Async(string path);
}