using System.ComponentModel.DataAnnotations;

namespace AadharVerification.Data
{
    public class CustomerInfo
    {
        [Key]
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public string? Name { get; set; }
        public string? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }     
        public string? District { get; set; }
        public string? State { get; set; }
        public string? PinCode { get; set; } 
        public string? Country { get; set; }
        public string? Photo { get; set; }       
    }
}
