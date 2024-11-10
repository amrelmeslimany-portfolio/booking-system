namespace api.Services.Common
{
    public interface IUploadFile
    {
        Task<string> Create(IFormFile file, string storePath);
        void Delete(string filePath);
    }
}
