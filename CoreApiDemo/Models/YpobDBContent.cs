using Microsoft.EntityFrameworkCore;

namespace CoreApiDemo.Models
{
    public class YpobDBContent : DbContext
    {
        public YpobDBContent(DbContextOptions<YpobDBContent> options) : base(options) { }

        public DbSet<User> User { get; set; }
    }
}
