using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AadharVerification.Data
{
    public class AadhaarAuthenticationContext : IdentityDbContext<IdentityUser>, IAadhaarAuthenticationContext
    {
        public AadhaarAuthenticationContext() { }
        public AadhaarAuthenticationContext(DbContextOptions<AadhaarAuthenticationContext> options) : base(options)
        {
        }
        public DbSet<CustomerInfo> CustomerInfo { get; set; }
    }
}
