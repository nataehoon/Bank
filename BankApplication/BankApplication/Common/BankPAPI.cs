using Microsoft.EntityFrameworkCore;

namespace BankApplication.Common
{
    public class BankPAPI : DbContext
    {
        public BankPAPI(DbContextOptions<BankPAPI> options) : base(options)
        {

        }
    }
}
