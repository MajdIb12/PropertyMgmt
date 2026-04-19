using Microsoft.AspNetCore.Hosting;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Infrastructure.Services;
public class LocalFileService : IFileService
{
    private readonly IWebHostEnvironment _environment;

    public LocalFileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
    {
        // 1. تحديد المسار: wwwroot/uploads/listings
        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "listings");
        
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        // 2. توليد اسم فريد للملف
        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        // 3. حفظ الـ Stream في ملف
        using (var outputStream = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(outputStream);
        }

        // 4. إرجاع الرابط الذي سيخزن في قاعدة البيانات
        return $"/uploads/listings/{uniqueFileName}";
    }

    public void DeleteImage(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl)) return;

        // تحويل الرابط (URL) إلى مسار فيزيائي على السيرفر
        // نزيل الـ '/' من البداية لدمجه مع المسار الأساسي
        var relativePath = imageUrl.TrimStart('/');
        var fullPath = Path.Combine(_environment.WebRootPath, relativePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}