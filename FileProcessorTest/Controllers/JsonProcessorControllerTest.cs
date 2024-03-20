using FileProcessor.Controllers;
using FileProcessor.Dto;
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
    public class JsonProcessorControllerTest
    {

        private readonly Mock<ILogger<JsonProcessorController>> _loggerMock;
        private readonly Mock<IJsonFileDetailProcessor> _fileProcessorMock;
        private readonly JsonProcessorController _controller;


        public JsonProcessorControllerTest()
        {
            _loggerMock = new Mock<ILogger<JsonProcessorController>>();
            _fileProcessorMock = new Mock<IJsonFileDetailProcessor>();
            _controller = new JsonProcessorController(_loggerMock.Object, _fileProcessorMock.Object);
        }

        [TestMethod]
        public async Task UploadJsonFile_ValidFile_ReturnsOkResult()
        {
            // Arrange
            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.Length).Returns(10); // Set file length to simulate a valid file

            // Act
            var result = await _controller.UploadJsonFile(formFileMock.Object) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("File successfully uploaded!", result.Value);
        }

        [TestMethod]
        public async Task UploadJsonFile_NoFile_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.UploadJsonFile(null) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("No file uploaded.", result.Value);
        }

        [TestMethod]
        public async Task GetJsonUploadCount_ReturnsOkResult()
        {
            // Arrange
            _fileProcessorMock.Setup(fp => fp.GetFileProcessedCounter()).Returns(5); // Set counter value for testing

            // Act
            var result = await _controller.GetJsonUploadCount() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Value);
        }

        [TestMethod]
        public async Task GetJsonFilteredItems_ReturnsOkResult()
        {
            // Arrange
            string itemType = "ItemType";
            _fileProcessorMock.Setup(fp => fp.GetFilteredItems(itemType)).Returns(new List<Item>()); // Set filtered items for testing

            // Act
            var result = await _controller.GetJsonFilteredItems(itemType) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<List<Item>>(result.Value);
        }
    }
}
