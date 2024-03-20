using Castle.Core.Logging;
using FileProcessor.Controllers;
using FileProcessor.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FileProcessorTest.Controllers
{
    [TestClass]
    public class CsvProcessorControllerTest
    {
        private readonly Mock<ILogger<CsvProcessorController>> _loggerMock;
        private readonly Mock<ICsvFileDetailProcessor> _fileProcessorMock;
        private readonly CsvProcessorController _controller;

        public CsvProcessorControllerTest()
        {
            _loggerMock = new Mock<ILogger<CsvProcessorController>>();
            _fileProcessorMock = new Mock<ICsvFileDetailProcessor>();
            _controller = new CsvProcessorController(_loggerMock.Object, _fileProcessorMock.Object);
        }


        [TestMethod]
        public async Task UploadCsvFile_ValidFile_ReturnsOkResult()
        {
            // Arrange
            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.Length).Returns(10); // Set file length to simulate a valid file

            // Act
            var result = await _controller.UploadCsvFile(formFileMock.Object) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("File successfully uploaded!", result.Value);
        }

        [TestMethod]
        public async Task UploadCsvFile_NoFile_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.UploadCsvFile(null) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("No file uploaded.", result.Value);
        }

        [TestMethod]
        public async Task GetCsvUploadCount_ReturnsOkResult()
        {
            // Arrange
            _fileProcessorMock.Setup(fp => fp.GetFileProcessedCounter()).Returns(5); // Set counter value for testing

            // Act
            var result = await _controller.GetCsvUploadCount() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Value);
        }

        [TestMethod]
        public async Task GetCsvAverageAge_ReturnsOkResult()
        {

            // Arrange
            double averageAge = 42;
            _fileProcessorMock.Setup(fp => fp.GetAverageSumOfUsers()).Returns(averageAge); // Set average age value for testing

            // Act
            var result = await _controller.GetCsvAverageAge() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(averageAge, result.Value);
        }
    }
}
