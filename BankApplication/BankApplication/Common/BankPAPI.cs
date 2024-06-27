using Microsoft.EntityFrameworkCore;

namespace BankApplication.Common
{
    public class BankPAPI : DbContext
    {
        public BankPAPI(DbContextOptions<BankPAPI> options) : base(options)
        {

        }

        public DbSet<Member> members { get; set; } = null!;
        public DbSet<Bank> banks { get; set; } = null!;
    }
}
