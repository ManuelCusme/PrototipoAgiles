using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SeguridadUta.Api.Data;
using SeguridadUta.Api.Hubs;
using SeguridadUta.Api.Models;
using System.Security.Claims;

namespace SeguridadUta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncidentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<AlertHub> _hubContext;

        public IncidentController(ApplicationDbContext context, IHubContext<AlertHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpPost("panic")]
        public async Task<IActionResult> TriggerPanic([FromBody] PanicDto model)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var user = await _context.Users.FindAsync(userId);

            if (user == null || !user.IsActive)
                return Unauthorized("Usuario no activo.");

            // Find geofence
            string? geofenceName = null;
            var geofences = await _context.Geofences.ToListAsync();
            
            foreach (var gf in geofences)
            {
                var distance = GetDistance(model.Latitude, model.Longitude, gf.Latitude, gf.Longitude);
                if (distance <= gf.Radius)
                {
                    geofenceName = gf.Name;
                    break;
                }
            }

            var incident = new Incident
            {
                UserId = userId,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                GeofenceName = geofenceName,
                Motivo = model.Motivo // Guardamos el motivo
            };

            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();

            // Notify via SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveAlert", 
                $"{user.Nombre1} {user.Apellido1}", 
                new { lat = model.Latitude, lng = model.Longitude }, 
                geofenceName ?? "Ubicación desconocida",
                model.Motivo, // Enviamos el motivo a la red
                user.Facultad // Enviamos la facultad
            );

            return Ok(new { message = "Alerta enviada", geofence = geofenceName });
        }

        [HttpGet("geofences")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGeofences()
        {
            return Ok(await _context.Geofences.ToListAsync());
        }

        private double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371e3; // Earth radius in meters
            var φ1 = lat1 * Math.PI / 180;
            var φ2 = lat2 * Math.PI / 180;
            var Δφ = (lat2 - lat1) * Math.PI / 180;
            var Δλ = (lon2 - lon1) * Math.PI / 180;

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                    Math.Cos(φ1) * Math.Cos(φ2) *
                    Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }
    }

    public class PanicDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Motivo { get; set; } = "Emergencia";
    }
}
