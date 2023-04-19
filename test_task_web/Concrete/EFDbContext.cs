using System.Data.Entity;
using test_task_web.Models;
 //закрепление данных в базе
namespace test_task_web.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Coin> Coins { get; set; }
    }
}