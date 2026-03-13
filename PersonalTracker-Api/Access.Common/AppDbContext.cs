using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContex<AppDbContext> options)
        : base(options) {}

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}