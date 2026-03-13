using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalTracker.Api.Models;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContex<AppDbContext> options)
        : base(options) {}

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}