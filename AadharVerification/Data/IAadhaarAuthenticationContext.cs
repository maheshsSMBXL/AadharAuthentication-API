using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AadharVerification.Data
{
    public interface IAadhaarAuthenticationContext
    {
        DbSet<CustomerInfo> CustomerInfo { get; set; }
        DbSet<Hotels> Hotels { get; set; }

        int SaveChanges();
        DataTable ExecuteReader
        (
            string sql
        );
    }
}
