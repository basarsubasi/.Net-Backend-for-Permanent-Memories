using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApplication1.Data;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models.UserRelatedModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Use camelCase
    options.JsonSerializerOptions.WriteIndented = true; // Enable indentation for better readability
    // Add any other customization options you need
});

// Add DbContext for the main application
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContextConnection"));
    // Replace "UseSqlServer" with the appropriate method for your database provider.
    // Update the connection string as needed.
});

// Add DbContext specifically for Identity
builder.Services.AddDbContext<IdentityAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDbContextConnection"));
    // Replace "UseSqlServer" with the appropriate method for your database provider.
    // Update the connection string as needed.
});

// Add Identity services using IdentityAppDbContext
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<IdentityAppDbContext>();

builder.Services.AddAuthorization(options =>
{
    // Restrict access for employees
    options.AddPolicy("EmployeePolicy", policy => policy.RequireRole("Employee"));
});

builder.Services.AddSwaggerGen(); // Add this line to register MVC services.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

// Add this line to enable MVC.
app.MapControllers();

// Create roles in IdentityAppDbContext
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Check if roles exist, and create them if not
    var roles = new[] { "Customer", "Employee" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();

app.Run();
