File Processor

Installation
Clone the repository.
Configure the API key in the app settings.

Authentication
All endpoints in this controller require API key authentication.

The CsvProcessorController handles CSV file processing operations and provides endpoints for uploading CSV files, retrieving upload counts, and calculating average age.

Endpoints
1. Upload CSV File
Endpoint: POST /CsvProcessor/UploadCsvFile
Description: Uploads a CSV file.
Request Body: FormData with a file parameter.
Response:
Status 200 OK: File successfully uploaded!
Status 400 Bad Request: No file uploaded.
Status 500 Internal Server Error: Failed to upload file.

2. Get CSV Upload Count
Endpoint: GET /CsvProcessor/GetCsvUploadCount
Description: Retrieves the number of uploaded CSV files.
Response:
Status 200 OK: Returns the upload count.
Status 500 Internal Server Error: Failed to retrieve upload count.

3. Get CSV Average Age
Endpoint: GET /CsvProcessor/GetCsvAverageAge
Description: Calculates and returns the average age of users in the CSV file.
Response:
Status 200 OK: Returns the average age.
Status 500 Internal Server Error: Failed to retrieve average age.


The JsonProcessorController provides endpoints for processing JSON files, including uploading files, retrieving upload counts, and fetching filtered items.

Endpoints
1. Upload JSON File
Endpoint: POST /JsonProcessor/UploadJsonFile
Description: Uploads a JSON file.
Request Body: FormData with a file parameter.
Response:
Status 200 OK: File successfully uploaded!
Status 400 Bad Request: No file uploaded.
Status 500 Internal Server Error: Failed to upload file.

2. Get JSON Upload Count
Endpoint: GET /JsonProcessor/GetJsonUploadCount
Description: Retrieves the number of uploaded JSON files.
Response:
Status 200 OK: Returns the upload count.
Status 500 Internal Server Error: Failed to retrieve upload count.

3. Get JSON Filtered Items
Endpoint: GET /JsonProcessor/GetJsonFilteredItems?itemType={itemType}
Description: Retrieves filtered items based on the item type.
Query Parameter: itemType (string)
Response:
Status 200 OK: Returns filtered items.
Status 500 Internal Server Error: Failed to retrieve filtered items.
