using Microsoft.AspNetCore.SignalR;

namespace SeguridadUta.Api.Hubs
{
    public class AlertHub : Hub
    {
        public async Task SendAlert(string userName, string location, string geofence)
        {
            await Clients.All.SendAsync("ReceiveAlert", userName, location, geofence);
        }
        
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
    }
}
