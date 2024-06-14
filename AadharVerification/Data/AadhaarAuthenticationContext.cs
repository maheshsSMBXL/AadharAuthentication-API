using Microsoft.EntityFrameworkCore;

namespace AadharVerification.Data
{
    public class AadhaarAuthenticationContext : DbContext
    {
        public AadhaarAuthenticationContext() { }
        public AadhaarAuthenticationContext(DbContextOptions<AadhaarAuthenticationContext> options)
            : base(options)
        {
        }
    }
}
