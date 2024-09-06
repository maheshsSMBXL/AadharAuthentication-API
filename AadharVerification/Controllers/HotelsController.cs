using AadharVerification.Data;
using AadharVerification.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;

namespace AadharVerification.Controllers
{
    public class HotelsController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly AadhaarAuthenticationContext _context;

        public HotelsController(AadhaarAuthenticationContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }
        [HttpPost]
        [Route("SaveHotelsData")]
        public async Task<IActionResult> SaveHotelsData([FromBody] Hotels request)
        {
            var hotel = new Hotels
            {
                HotelId = Guid.NewGuid(),
                HotelName = request.HotelName,
                TypeOfHotel = request.TypeOfHotel,
                Location = request.Location,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Country = request.Country,
                PinCode = request.PinCode,
                MailId = request.MailId,
                Rating = request.Rating,
                NumberOfRatings = request.NumberOfRatings,
                MinimumPrice = request.MinimumPrice,
                Discount = request.Discount,
                RoomAvailability = request.RoomAvailability,
                Photo = request.Photo
            };

            await _context.Hotels.AddAsync(hotel);
            _context.SaveChanges();

            return Ok(new { Status = "Success", Message = "Data Saved Successfully." });
        }
        [HttpPost]
        [Route("SaveHotelsData2")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SaveHotelsData2([FromForm] HotelRequest request, [FromServices] IWebHostEnvironment env)
        {
            string folderPath = Path.Combine(env.ContentRootPath, "Photos");
            // Ensure the directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine("Directory created at: " + folderPath);
            }

            // Create a file path for the uploaded file
            string filePath = Path.Combine(folderPath, request.Image.FileName);

            // Save the file to the specified path
            using (Stream stream = new FileStream(filePath, FileMode.Create))
            {
                request.Image.CopyTo(stream);
            }

            var hotel = new Hotels
            {
                HotelId = Guid.NewGuid(),
                HotelName = request.HotelName,
                TypeOfHotel = request.TypeOfHotel,
                Location = request.Location,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Country = request.Country,
                PinCode = request.PinCode,
                MailId = request.MailId,
                Rating = request.Rating,
                NumberOfRatings = request.NumberOfRatings,
                MinimumPrice = request.MinimumPrice,
                Discount = request.Discount,
                RoomAvailability = request.RoomAvailability,
                Photo = request.Image.FileName,
                Description = request.Description,
            };

            await _context.Hotels.AddAsync(hotel);
            _context.SaveChanges();

            return Ok(new { Status = "Success", Message = "Data Saved Successfully." });
        }
        [HttpGet]
        [Route("GetAllHotels")]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();

            return Ok(new { Status = "Success", Data = hotels });
        }        
        [HttpGet]
        [Route("GetHotelsInCity/{cityName}")]
        public async Task<IActionResult> GetHotelsInCity(string cityName)
        {
            List<Hotels> topTwoHotels = new List<Hotels>();
            List<Hotels> remainingHotels = new List<Hotels>();

            topTwoHotels = await _context.Hotels
                                .Where(h => h.City == cityName)
                                .OrderByDescending(h => h.Rating)
                                .Take(2)
                                .ToListAsync();

            remainingHotels = await _context.Hotels
                                   .Where(h => h.City == cityName && !topTwoHotels.Select(th => th.HotelId).Contains(h.HotelId))
                                   .ToListAsync();
            var result = new
            {
                Status = "Success",
                FeaturedHotels = topTwoHotels,
                SimilarHotels = remainingHotels
            };

            return Ok(result);
        }
    }
}
