using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Repositories;

public class FileService:IFileService
{
    public async Task<string> AddImage(IFormFile file, string path)
    {
        if (file == null || file.Length == 0)
            return string.Empty;

        string folderPath = Path.Combine("wwwroot", "image", path);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string extension = Path.GetExtension(file.FileName);
        string fileName = $"{file.FileName}{extension}";

        string fullPath = Path.Combine(folderPath, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }

    public void RemoveImage(string imageName, string path)
    {
        var pathCombine = Path.Combine("wwwroot", path, imageName);

        if (imageName != "noimage.jpg" && imageName != "DefaultIngPic.png" && imageName != "o2fit image.jpg")
        {
            if (File.Exists(pathCombine))
            {
                File.Delete(pathCombine);
            }
        }
    }

    public async Task<string> GetImageAsBase64Async(string path)
    {
        var imageBytes = await File.ReadAllBytesAsync($"wwwroot/{path}");
        return Convert.ToBase64String(imageBytes);
    }
    
}