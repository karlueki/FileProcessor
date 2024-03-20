using CsvHelper;
using FileProcessor.Dto;
using FileProcessor.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace FileProcessor.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CsvProcessorController : ControllerBase
    {
        private readonly ILogger<CsvProcessorController> _logger;

        private readonly ICsvFileDetailProcessor _fileProcessor;

        public CsvProcessorController(ILogger<CsvProcessorController> logger, ICsvFileDetailProcessor fileProcessor)
        {
            _logger = logger;
            _fileProcessor = fileProcessor;
        }

        [HttpPost]
        [Route("UploadCsvFile")]
        public async Task<ActionResult> UploadCsvFile([FromForm] IFormFile file)
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
                _logger.LogError(ex, $"{nameof(UploadCsvFile)} failed to complete.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to upload file.");
            }
        }

        [HttpGet]
        [Route("GetCsvUploadCount")]
        public async Task<ActionResult> GetCsvUploadCount()
        {
            try
            {
                int count = _fileProcessor.GetFileProcessedCounter();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCsvUploadCount)} failed to complete.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to retrieve upload count.");
            }
        }

      

        [HttpGet]
        [Route("GetCsvAverageAge")]
        public async Task<ActionResult> GetCsvAverageAge()
        {
            try
            {
                double averageAge = _fileProcessor.GetAverageSumOfUsers();
                return Ok(averageAge);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCsvAverageAge)} failed to complete.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to retrieve average age.");
            }
        }
    }
}
