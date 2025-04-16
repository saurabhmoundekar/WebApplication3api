using Microsoft.EntityFrameworkCore;

namespace WebApplication3API.Models
{
    public class RegisterContext : DbContext
    {
        public RegisterContext(DbContextOptions<RegisterContext> options) : base(options)
        {
        }

        public DbSet<Register> Register { get; set; }
    }
}
