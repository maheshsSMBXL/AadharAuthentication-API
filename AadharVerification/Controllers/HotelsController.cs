using AadharVerification.Data;
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
        [HttpGet]
        [Route("GetAllHotels")]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();

            if (hotels == null || !hotels.Any())
            {
                return NotFound(new { Status = "Error", Message = "No hotels found." });
            }

            return Ok(new { Status = "Success", Data = hotels });
        }
    }
}
