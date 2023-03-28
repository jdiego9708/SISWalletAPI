using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace SISWallet.API.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly HubConnection _hubConnection;

        public NotificationHub(HubConnection hubConnection)
        {
            _hubConnection = hubConnection;
        }

        public async Task ConnectAsync()
        {
            await _hubConnection.StartAsync();
        }

        public async Task DisconnectAsync()
        {
            await _hubConnection.StopAsync();
        }

        public async Task<string> SendNotificationAsync(string message)
        {
            return await _hubConnection.InvokeAsync<string>("SendNotification", message);
        }
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
