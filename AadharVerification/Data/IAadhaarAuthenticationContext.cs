using Microsoft.EntityFrameworkCore;

namespace AadharVerification.Data
{
    public interface IAadhaarAuthenticationContext
    {
        DbSet<CustomerInfo> CustomerInfo { get; set; }
    }
}
