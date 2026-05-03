using System.ComponentModel.DataAnnotations;

namespace SeguridadUta.Api.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Nombre1 { get; set; } = string.Empty;
        public string? Nombre2 { get; set; }
        [Required]
        public string Apellido1 { get; set; } = string.Empty;
        public string? Apellido2 { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Role { get; set; } = "Estudiante";
        public string Facultad { get; set; } = "FISEI";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Geofence
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Radius { get; set; }
    }

    public class Incident
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        public string? GeofenceName { get; set; }
        public string Motivo { get; set; } = "Emergencia";
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
