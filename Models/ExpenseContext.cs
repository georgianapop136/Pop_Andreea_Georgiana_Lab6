using Microsoft.EntityFrameworkCore;
namespace Pop_Andreea_Georgiana_Lab6.Models
{
    public class ExpenseContext : DbContext
    {
      public ExpenseContext(DbContextOptions<ExpenseContext> options)
            : base(options)
        {
        }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<ExpenseDTO> ExpenseDTO { get; set; }

    }
}
