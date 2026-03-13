using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Manager.Contract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<TaskAccessor>();
builder.Services.AddScoped<TaskManager>();

builder.Services.AddScoped<AuthManager>();
builder.Services.AddScoped<JwtService>();
