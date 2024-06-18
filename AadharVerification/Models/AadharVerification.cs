namespace AadharVerification.Models
{
    public class AadharVerification
    {
    }
    public class ImageVerificationRequest
    {
        public string scanBase64 { get; set; }
        public string AadhaarBase64 { get; set; }
    }
    public class AadharOtpRequest
    {
        public string AadharOTP { get; set; }
        public string TransactionId { get; set; }
    }
    public class SubmitOtpRequest
    {
        public string aadhaarId { get; set; }
    }
    public class AadhaarOtpResponse
    {
        public string? Result { get; set; }
        public Guid CustomerInfoId { get; set; }
    }
}
