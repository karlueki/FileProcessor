using CsvHelper.Configuration;
using CsvHelper;
using FileProcessor.Dto;
using FileProcessor.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace FileProcessorTest.FileProcessors
{
    [TestClass]
    public class CsvProcessorTest
    {
        private CSVFileDetailProcessor _processor;

        public CsvProcessorTest()
        {
            _processor = new CSVFileDetailProcessor();
        }

        [TestMethod]
        public void ProcessFile_IncrementsFileCounter()
        {
            // Arrange
            int initialCounter = _processor.GetFileProcessedCounter();

            // Act
            _processor.ProcessFile("test.csv");

            // Assert
            int newCounter = _processor.GetFileProcessedCounter();
            Assert.AreEqual(initialCounter + 1, newCounter);
        }

        [TestMethod]
        public async Task UploadFileAsync_AddsRecordsToUserUploads()
        {
            // Arrange
            var users = new List<User>
        {
            new User { Name = "Karl", Age = 25 },
            new User { Name = "Kimchi", Age = 30 },
            new User { Name = "Chunky", Age = 35 }
        };
            var file = CreateMockFormFile(users);

            // Act
            await _processor.UploadFileAsync(file);

            // Assert
            Assert.AreEqual(1, _processor.GetFileProcessedCounter());
            Assert.AreEqual(30, _processor.GetAverageSumOfUsers());
        }

        private IFormFile CreateMockFormFile(List<User> users)
        {
            var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(memoryStream, Encoding.UTF8, 1024, true))
            {
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.WriteRecords(users);
                }
            }
            memoryStream.Position = 0;
            return new FormFile(memoryStream, 0, memoryStream.Length, "data", "users.csv");
        }
    }
}
