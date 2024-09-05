namespace AadharVerification.Models
{
    public class HotelRequest
    {
        public string? HotelName { get; set; }
        public string? TypeOfHotel { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PinCode { get; set; }
        public string? MailId { get; set; }
        public double? Rating { get; set; }
        public int? NumberOfRatings { get; set; }
        public double? MinimumPrice { get; set; }
        public double? Discount { get; set; }
        public bool? RoomAvailability { get; set; }
        public IFormFile? Image { get; set; }
    }
}
