using CsvHelper;
using FileProcessor.Dto;
using FileProcessor.Interface;
using Newtonsoft.Json;

namespace FileProcessor.Services
{
    public class CSVFileDetailProcessor : ICsvFileDetailProcessor
    {
        private int _fileCounter;
        private UserUploads _userUploads = new UserUploads();

        public CSVFileDetailProcessor()
        {
            _fileCounter = 0;
        }

        public double GetAverageSumOfUsers()
        {
            return  _userUploads.Users.Average(person => person.Age);
        }

        public int GetFileProcessedCounter()
        {
            return _fileCounter;

        }

        public void ProcessFile(string fileName)
        {
            _fileCounter++;
        }

        public async Task UploadFileAsync(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
                {
                    var records = csv.GetRecords<User>().ToList();
                    _userUploads.Users.AddRange(records);

                    ProcessFile(file.FileName);
                }
            }
        }
    }
}
