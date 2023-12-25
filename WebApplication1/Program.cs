using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore.Design;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers() .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Use camelCase
        options.JsonSerializerOptions.WriteIndented = true; // Enable indentation for better readability
        // Add any other customization options you need
        
    });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddSwaggerGen(); // Add this line to register MVC services.
// Add DbContext and configure its connection
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    // Replace "UseSqlServer" with the appropriate method for your database provider.
    // Update the connection string as needed.
});
// Your existing Swagger configuration...

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

app.Run();
