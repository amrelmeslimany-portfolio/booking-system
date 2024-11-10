namespace api.Services.Common
{
    public class UploadFile(IWebHostEnvironment webHost) : IUploadFile
    {
        public async Task<string> Create(IFormFile file, string storePath)
        {
            try
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string webpath = Path.Combine(webHost.WebRootPath, storePath);
                if (!Directory.Exists(webpath))
                    Directory.CreateDirectory(webpath);
                using var fileStream = new FileStream(
                    Path.Combine(webpath, fileName),
                    FileMode.Create
                );
                await file.CopyToAsync(fileStream);
                return Path.Combine(storePath, fileName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(string filePath)
        {
            string webPath = Path.Combine(webHost.WebRootPath, filePath);
            if (File.Exists(webPath))
                File.Delete(webPath);
        }
    }
}
