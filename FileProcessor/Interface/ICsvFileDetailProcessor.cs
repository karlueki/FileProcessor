using FileProcessor.Dto;

namespace FileProcessor.Interface
{
    public interface ICsvFileDetailProcessor
    {
        public void ProcessFile(string fileName);
        public int GetFileProcessedCounter();
        public Task UploadFileAsync(IFormFile file);
        public double GetAverageSumOfUsers();
    }
}
