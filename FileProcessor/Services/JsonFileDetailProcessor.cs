using FileProcessor.Dto;
using FileProcessor.Interface;
using Newtonsoft.Json;

namespace FileProcessor.Services
{
    public class JsonFileDetailProcessor : IJsonFileDetailProcessor
    {

        private int _fileCounter;
        private ItemUploads _itemUploads = new ItemUploads();

        public JsonFileDetailProcessor()
        {
            _fileCounter = 0;
        }

        public void ProcessFile(string fileName)
        {
            _fileCounter++;
        }

        public async Task UploadFileAsync(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var fileContent = await reader.ReadToEndAsync();
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(fileContent);
                _itemUploads.Items.AddRange(items);

                ProcessFile(file.FileName);

            }
        }

        public int GetFileProcessedCounter()
        {
            return _fileCounter;
        }

        public List<Item> GetFilteredItems(string itemType)
        {
            return _itemUploads.Items.FindAll(item => item.Type.Contains(itemType));
        }

    }
}
