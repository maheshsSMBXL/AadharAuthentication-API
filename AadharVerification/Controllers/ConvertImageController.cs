using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public void ImageToBase64(IFormFile file) 
        {

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    byte[] fileByte = ms.ToArray();
                    string Base64 = Convert.ToBase64String(fileByte);
                    // testing

                    //hello world.
                }
            }                      
        }

        [HttpPost("AadhaarId")]
        public async Task<string> FetchDetailsByAadhaarId(string aadhaarId) 
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.gridlines.io/aadhaar-api/boson/generate-otp");
            request.Headers.Add("X-API-Key", "LhSjNKH5goyEU0IQkG9mTkSi84NKLJlH");
            request.Headers.Add("X-Auth-Type", "API-Key");
            var content = new StringContent("{\r\n  \"aadhaar_number\": \"422752102653\",\r\n  \"consent\": \"Y\"\r\n}", null, "application/json");
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
        public async Task<string> AadhaarOtpDataFetch(string AadharOTP,string TransactionId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.gridlines.io/aadhaar-api/boson/submit-otp");
            request.Headers.Add("X-API-Key", "LhSjNKH5goyEU0IQkG9mTkSi84NKLJlH");
            request.Headers.Add("X-Auth-Type", "API-Key");
            request.Headers.Add("X-Transaction-ID", TransactionId);
            //var content = new StringContent("{\r\n  \"otp\": 216165,\r\n  \"include_xml\": true,\r\n  \"share_code\": \"1234\"\r\n}", null, "application/json");
            var content = new StringContent($"{{\r\n  \"otp\": {AadharOTP},\r\n  \"include_xml\": true,\r\n  \"share_code\": \"1234\"\r\n}}", Encoding.UTF8, "application/json");
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
        public async Task<string> Base65Data(string scanBase64 , string AadhaarBase64)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.gridlines.io/aadhaar-api/boson/submit-otp");
            request.Headers.Add("X-API-Key", "LhSjNKH5goyEU0IQkG9mTkSi84NKLJlH");
            request.Headers.Add("X-Auth-Type", "API-Key");
            var content = new StringContent($"{{\r\n  \"file_1_base64\": \"{scanBase64}\",\r\n  \"file_2_base64\": \"{AadhaarBase64}\",\r\n  \"consent\": \"Y\"\r\n}}", Encoding.UTF8, "application/json");
            request.Content = content;
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


    }
}
