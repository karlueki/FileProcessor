using FileProcessor.Dto;
using FileProcessor.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace FileProcessorTest.FileProcessors
{
    [TestClass]
    public class JsonProcessorTest
    {
        private JsonFileDetailProcessor _processor;

        public JsonProcessorTest()
        {
                _processor = new JsonFileDetailProcessor(); 
        }

        [TestMethod]
        public void ProcessFile_IncrementsFileCounter()
        {
            // Arrange
            int initialCounter = _processor.GetFileProcessedCounter();

            // Act
            _processor.ProcessFile("test.json");

            // Assert
            int newCounter = _processor.GetFileProcessedCounter();
            Assert.AreEqual(initialCounter + 1, newCounter);
        }

        [TestMethod]
        public async Task UploadFileAsync_AddsItemsToItemUploads()
        {
            // Arrange
            var items = new List<Item>
        {
            new Item { Name = "Item1", Type = "Type1", Code="Original" },
            new Item { Name = "Item2", Type = "Type2", Code="Original" },
            new Item { Name = "Item3", Type = "Type1", Code="Original" }
        };
            var file = CreateMockFormFile(items);

            // Act
            await _processor.UploadFileAsync(file);

            // Assert
            Assert.AreEqual(1, _processor.GetFileProcessedCounter());
            var filteredItems = _processor.GetFilteredItems("Type1");
            Assert.AreEqual(2, filteredItems.Count);
            Assert.IsTrue(filteredItems.All(item => item.Type.Contains("Type1")));
        }

        private IFormFile CreateMockFormFile(List<Item> items)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream))
                {
                    var json = JsonConvert.SerializeObject(items);
                    writer.Write(json);
                    writer.Flush();
                    memoryStream.Position = 0;
                }

                // Create a new MemoryStream and copy content from the original stream
                var fileMemoryStream = new MemoryStream(memoryStream.ToArray());
                return new FormFile(fileMemoryStream, 0, fileMemoryStream.Length, "data", "items.json");
            }
        }
    }
}