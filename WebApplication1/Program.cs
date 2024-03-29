using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.UserRelatedModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());

});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Add DbContext for the main application
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContextConnection"));
});

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDbContextConnection"))); // Replace with your actual connection string

// Add DbContext specifically for Identity
builder.Services.AddDbContext<IdentityAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDbContextConnection"));
});

// Add Identity services using IdentityAppDbContext
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<IdentityAppDbContext>();

// Add Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("EmployeeOrAdmin", policy => policy.RequireRole("Employee", "Admin"));
    
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
    Title = "WebApplication1", 
    Version = "v1",
    Description = "My WebAPI",
    Contact = new Microsoft.OpenApi.Models.OpenApiContact
    {
        Name = "Başar SUBAŞI",
        Email = "basarsubasi@gmail.com",  
    }
    });
     
});

var app = builder.Build();

// Create a scope to get services
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Initialize the database
    var dbContext = services.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();

    // Initialize Identity database
    var identityDbContext = services.GetRequiredService<IdentityAppDbContext>();
    identityDbContext.Database.Migrate();

    // Initialize roles and an admin user
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
           SeedData.InitializeRolesAndAdminUser(roleManager, userManager).Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontendOrigin");
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();


