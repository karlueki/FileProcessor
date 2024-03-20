using FileProcessor.Dto;

namespace FileProcessor.Interface
{
    public interface IJsonFileDetailProcessor
    {
        public void ProcessFile(string fileName);
        public int GetFileProcessedCounter();
        public Task UploadFileAsync(IFormFile file);
        public List<Item> GetFilteredItems(string itemType);
    }
}
