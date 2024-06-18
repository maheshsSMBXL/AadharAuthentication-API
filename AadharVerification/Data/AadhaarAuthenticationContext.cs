using Microsoft.EntityFrameworkCore;

namespace AadharVerification.Data
{
    public class AadhaarAuthenticationContext : DbContext, IAadhaarAuthenticationContext
    {
        public AadhaarAuthenticationContext() { }
        public AadhaarAuthenticationContext(DbContextOptions<AadhaarAuthenticationContext> options)
            : base(options)
        {
        }
        public DbSet<CustomerInfo> CustomerInfo { get; set; }
    }
}
