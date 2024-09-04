using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AadharVerification.Data
{
    public class AadhaarAuthenticationContext : IdentityDbContext<IdentityUser>, IAadhaarAuthenticationContext
    {
        public AadhaarAuthenticationContext() { }
        public AadhaarAuthenticationContext(DbContextOptions<AadhaarAuthenticationContext> options) : base(options)
        {
        }
        public DbSet<CustomerInfo> CustomerInfo { get; set; }
        public DbSet<Hotels> Hotels { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        public DataTable ExecuteReader
        (
        string sql
        )
        {
            IDbConnection connection = Database.GetDbConnection();
            IDbCommand command = connection.CreateCommand();
            try
            {
                connection.Open();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                IDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                var dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
                //result = Convert<TType>(reader);
            }
            finally
            {
                connection.Close();
            }
            return null;
        }
    }
}
