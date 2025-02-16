using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Data;
public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    { }
    public DbSet<User> Users { get; set; }
}