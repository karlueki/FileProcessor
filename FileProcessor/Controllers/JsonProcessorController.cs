using CsvHelper;
using FileProcessor.Dto;
using FileProcessor.Interface;
using FileProcessor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FileProcessor.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class JsonProcessorController : ControllerBase
    {
        private readonly ILogger<JsonProcessorController> _logger;

        private readonly IJsonFileDetailProcessor _fileProcessor;

        public JsonProcessorController(ILogger<JsonProcessorController> logger,IJsonFileDetailProcessor fileProcessor)
        {
            _logger = logger;
            _fileProcessor = fileProcessor;
        }

        [HttpPost]
        [Route("UploadJsonFile")]
        public async Task<ActionResult> UploadJsonFile([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }
               await _fileProcessor.UploadFileAsync(file);
                return Ok("File successfully uploaded!");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UploadJsonFile)} failed to complete.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to upload file.");
            }
        }

        [HttpGet]
        [Route("GetJsonUploadCount")]
        public async Task<ActionResult> GetJsonUploadCount()
        {
            try
            {
                return Ok(_fileProcessor.GetFileProcessedCounter());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetJsonUploadCount)} failed to complete.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to retrieve upload count.");
            }
        }

        [HttpGet]
        [Route("GetJsonFilteredItems")]
        public async Task<ActionResult> GetJsonFilteredItems([FromForm] string itemType)
        {
            try
            {
                return Ok(_fileProcessor.GetFilteredItems(itemType));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetJsonUploadCount)} failed to complete.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to retrieve filtered items.");
            }
        }
    }
}
