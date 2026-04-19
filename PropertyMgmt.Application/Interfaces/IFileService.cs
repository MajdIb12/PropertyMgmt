namespace PropertyMgmt.Application.Interfaces;

public interface IFileService
{
    Task<string> UploadImageAsync(Stream fileStream, string fileName);
    void DeleteImage(string imageUrl);
}