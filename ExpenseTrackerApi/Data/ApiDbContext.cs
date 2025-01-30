using Microsoft.EntityFrameworkCore;

namespace AppDemo.Data;
public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    { }
    public DbSet<User> Users { get; set; }
}