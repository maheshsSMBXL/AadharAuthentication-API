using AadharVerification.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Transactions;
using System.Web;
using static System.Net.WebRequestMethods;

namespace AadharVerification.Controllers
{
    public class ConvertImageController : Controller
    {
        private readonly HttpClient _httpClient;

        public ConvertImageController()
        {
            _httpClient = new HttpClient();
            // You can set base address or other configurations for HttpClient here if needed
            // _httpClient.BaseAddress = new Uri("http://your-service-base-url/");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("ImageToBase64")]
        public string ImageToBase64(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    byte[] fileByte = ms.ToArray();
                    string Base64 = Convert.ToBase64String(fileByte);
                    return Base64;
                    // testing

                    //hello world.
                }
            }
            return null;
        }

        [HttpPost("AadhaarId")]
        public async Task<string> FetchDetailsByAadhaarId([FromBody] SubmitOtpRequest input) 
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.gridlines.io/aadhaar-api/boson/generate-otp");
            request.Headers.Add("X-API-Key", "B884yJGP8x0yAFUGx72qSEOfmtsUaOUG");
            request.Headers.Add("X-Auth-Type", "API-Key");
            var content = new StringContent($"{{\"aadhaar_number\": \"{input.aadhaarId}\", \"consent\": \"Y\"}}", null, "application/json");
            request.Content = content;
            
            var response = await client.SendAsync(request);
            var data = response.Headers.NonValidated;
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);

            // Parse the JSON response
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);

            // Access the necessary fields
            string requestId = responseData.request_id;
            int status = responseData.status;
            string code = responseData.data.code;
            string message = responseData.data.message;
            string transactionId = responseData.data.transaction_id;
            long timestamp = responseData.timestamp;
            string path = responseData.path;

            // Do something with the extracted data
            Console.WriteLine("Request ID: " + requestId);
            Console.WriteLine("Status: " + status);
            Console.WriteLine("Code: " + code);
            Console.WriteLine("Message: " + message);
            Console.WriteLine("Transaction ID: " + transactionId);
            Console.WriteLine("Timestamp: " + timestamp);
            Console.WriteLine("Path: " + path);
            return transactionId.ToString();
        }
        [HttpPost("AadhaarOTP")]
        public async Task<string> AadhaarOtpDataFetch([FromBody] AadharOtpRequest input)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.gridlines.io/aadhaar-api/boson/submit-otp");
            request.Headers.Add("X-API-Key", "B884yJGP8x0yAFUGx72qSEOfmtsUaOUG");
            request.Headers.Add("X-Auth-Type", "API-Key");
            request.Headers.Add("X-Transaction-ID", input.TransactionId);
            //var content = new StringContent("{\r\n  \"otp\": 216165,\r\n  \"include_xml\": true,\r\n  \"share_code\": \"1234\"\r\n}", null, "application/json");
            var content = new StringContent($"{{\r\n  \"otp\": {input.AadharOTP},\r\n  \"include_xml\": true,\r\n  \"share_code\": \"1234\"\r\n}}", Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
            string requestId = responseData.request_id;
            int status = responseData.status;
            string code = responseData.data.code;
            string message = responseData.data.message;
            string transactionId = responseData.data.transaction_id;
            long timestamp = responseData.timestamp;
            string path = responseData.path;

            string documentType = responseData.data.aadhaar_data.document_type;
            string referenceId = responseData.data.aadhaar_data.reference_id;
            string name = responseData.data.aadhaar_data.name;
            string dateOfBirth = responseData.data.aadhaar_data.date_of_birth;
            string gender = responseData.data.aadhaar_data.gender;
            string mobile = responseData.data.aadhaar_data.mobile;
            string careOf = responseData.data.aadhaar_data.care_of;
            string house = responseData.data.aadhaar_data.house;
            string street = responseData.data.aadhaar_data.street;
            string district = responseData.data.aadhaar_data.district;
            string landmark = responseData.data.aadhaar_data.landmark;
            string locality = responseData.data.aadhaar_data.locality;
            string postOfficeName = responseData.data.aadhaar_data.post_office_name;
            string state = responseData.data.aadhaar_data.state;
            string pincode = responseData.data.aadhaar_data.pincode;
            string country = responseData.data.aadhaar_data.country;
            string vtcName = responseData.data.aadhaar_data.vtc_name;
            string PhotoString = responseData.data.aadhaar_data.photo_base64;
             
            // Do something with the extracted data
            Console.WriteLine("Request ID: " + requestId);
            Console.WriteLine("Status: " + status);
            Console.WriteLine("Code: " + code);
            Console.WriteLine("Message: " + message);
            Console.WriteLine("Transaction ID: " + transactionId);
            Console.WriteLine("Timestamp: " + timestamp);
            Console.WriteLine("Path: " + path);

            Console.WriteLine("Document Type: " + documentType);
            Console.WriteLine("Reference ID: " + referenceId);
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Date of Birth: " + dateOfBirth);
            Console.WriteLine("Gender: " + gender);
            Console.WriteLine("Mobile: " + mobile);
            Console.WriteLine("Care Of: " + careOf);
            Console.WriteLine("House: " + house);
            Console.WriteLine("Street: " + street);
            Console.WriteLine("District: " + district);
            Console.WriteLine("Landmark: " + landmark);
            Console.WriteLine("Locality: " + locality);
            Console.WriteLine("Post Office Name: " + postOfficeName);
            Console.WriteLine("State: " + state);
            Console.WriteLine("Pincode: " + pincode);
            Console.WriteLine("Country: " + country);
            Console.WriteLine("VTC Name: " + vtcName);
            Console.WriteLine(responseData);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return result.ToString();

        }

        [HttpPost("ImageVerification")]
        public async Task<string> Base65Data([FromBody] ImageVerificationRequest input)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.gridlines.io/face-api/verify");
            request.Headers.Add("X-API-Key", "B884yJGP8x0yAFUGx72qSEOfmtsUaOUG");
            request.Headers.Add("X-Auth-Type", "API-Key");
            var jsonBody = new
            {
                file_1_base64 = input.scanBase64,
                file_2_base64 = input.AadhaarBase64,
                consent = "Y"
            };
            var jsonBodyString = JsonConvert.SerializeObject(jsonBody);

            // Setting the content of the request
            var content = new StringContent(jsonBodyString, Encoding.UTF8, "application/json");
            request.Content = content;
            //var content = new StringContent($"{{\r\n  \"file_1_base64\": \"{input.scanBase64}\",\r\n  \"file_2_base64\": \"{input.AadhaarBase64}\",\r\n  \"consent\": \"Y\"\r\n}}", null, "application/json");
            //request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
            // Accessing properties
            string requestId = responseData.request_id;
            int status = responseData.status;
            string code = responseData.data.code;
            double confidence = responseData.data.confidence;
            string message = responseData.data.message;
            long timestamp = responseData.timestamp;
            string path = responseData.path;

            // Displaying values
            Console.WriteLine("Request ID: " + requestId);
            Console.WriteLine("Status: " + status);
            Console.WriteLine("Code: " + code);
            Console.WriteLine("Confidence: " + confidence);
            Console.WriteLine("Message: " + message);
            Console.WriteLine("Timestamp: " + timestamp);
            Console.WriteLine("Path: " + path);
            return message;
        }

        [HttpPost("SavePhoto")]
        public IActionResult SavePhoto([FromForm] FileModel fileModel, [FromServices] IWebHostEnvironment env)
        {
            string folderPath = Path.Combine(env.ContentRootPath, "Photos");
            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    Console.WriteLine("Directory created at: " + folderPath);
                }

                // Create a file path for the uploaded file
                string filePath = Path.Combine(folderPath, fileModel.file.FileName);

                // Save the file to the specified path
                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    fileModel.file.CopyTo(stream);
                }

                return Ok(new { message = "Image saved successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Error: Access to the path is denied. " + ex.Message);
                return StatusCode(500, "Access to the path is denied.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return StatusCode(500, "An error occurred while saving the image.");
            }
        }
        [HttpPost("SaveImage")]
        public IActionResult SaveImage([FromForm] FileModel fileModel, [FromServices] IWebHostEnvironment env)
        {
            string folderPath = @"C:\inetpub\wwwroot\Hosting\AadhaarAuthenticationAPI\Photos";

            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    Console.WriteLine("Directory created at: " + folderPath);
                }

                // Create a file path for the uploaded file
                string filePath = Path.Combine(folderPath, fileModel.file.FileName);

                // Save the file to the specified path
                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    fileModel.file.CopyTo(stream);
                }

                return Ok(new { message = "Image saved successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Error: Access to the path is denied. " + ex.Message);
                return StatusCode(500, "Access to the path is denied.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return StatusCode(500, "An error occurred while saving the image.");
            }
        }


    }
}
