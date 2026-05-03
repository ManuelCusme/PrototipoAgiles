using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SeguridadUta.Api.Data;
using SeguridadUta.Api.Hubs;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// SignalR
builder.Services.AddSignalR();

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "super_secret_key_1234567890123456";
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<AlertHub>("/alertHub");

// Automatic Database Creation
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    
    // ATENCION: Esto borra y recrea la BD para asegurar los 11 usuarios
    context.Database.EnsureDeleted(); 
    context.Database.EnsureCreated();
    
    // Seed Geofences if empty
    if (!context.Geofences.Any())
    {
        context.Geofences.AddRange(
            new SeguridadUta.Api.Models.Geofence { Name = "Campus Huachi", Latitude = -1.2692, Longitude = -78.6242, Radius = 500 },
            new SeguridadUta.Api.Models.Geofence { Name = "Campus Ingahurco", Latitude = -1.2422, Longitude = -78.6251, Radius = 300 },
            new SeguridadUta.Api.Models.Geofence { Name = "Facultad Sistemas", Latitude = -1.2655, Longitude = -78.6210, Radius = 100 }
        );
        context.SaveChanges();
    }

    // Seed Users (DITIC Simulation)
    if (!context.Users.Any())
    {
        var users = new List<SeguridadUta.Api.Models.User>();
        
        // 1 Admin
        users.Add(new SeguridadUta.Api.Models.User { 
            Id = Guid.NewGuid(), Email = "admin@uta.edu.ec", 
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), 
            Nombre1 = "Administrador", Apellido1 = "Central", Role = "Admin",
            BirthDate = new DateTime(1990, 1, 1)
        });

        // 5 Estudiantes
        for(int i=1; i<=5; i++) {
            users.Add(new SeguridadUta.Api.Models.User { 
                Id = Guid.NewGuid(), Email = $"estudiante{i}@uta.edu.ec", 
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"), 
                Nombre1 = $"Estudiante", Apellido1 = $"{i}", Role = "Estudiante",
                BirthDate = new DateTime(2000, 1, 1)
            });
        }

        // 5 Guardias
        for(int i=1; i<=5; i++) {
            users.Add(new SeguridadUta.Api.Models.User { 
                Id = Guid.NewGuid(), Email = $"guardia{i}@uta.edu.ec", 
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"), 
                Nombre1 = $"Guardia", Apellido1 = $"{i}", Role = "Guardia",
                BirthDate = new DateTime(1985, 1, 1)
            });
        }

        context.Users.AddRange(users);
        context.SaveChanges();
    }
}

app.Run();
