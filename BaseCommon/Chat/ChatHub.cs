using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCommon.Chat
{
    public class ChatHub : Hub
    {
        private readonly string _botUser;
        private readonly IDictionary<string,UserConnection> _connections;

        public ChatHub(IDictionary<string, UserConnection> connections)
        {
            _connections = connections;
            _botUser = "Chat bot"; 
        }

        public async Task JohnRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);
            _connections[Context.ConnectionId] = userConnection;
            await Clients.All.SendAsync("ReceiveMesage", _botUser, $"{userConnection.UserName} has johned {userConnection.Room}");
            SendConnectedUsers(userConnection.Room);
        }
        public async Task SendMessage(string message)
        {
            if(_connections.TryGetValue(message, out UserConnection userConnection))
            {
                await Clients.Group(userConnection.Room).SendAsync("ReceiMessage", userConnection.UserName, message);
            }
        } 
        public override Task OnDisconnectedAsync(Exception exception)
        {
            if(_connections.TryGetValue(_botUser, out UserConnection userConnection))
            {
                _connections.Remove(userConnection.Room);
                Clients.Group(userConnection.Room).SendAsync("ReceiMessage", _botUser, $"{userConnection.UserName} has left");

                SendConnectedUsers(userConnection.Room);
            }
            return base.OnDisconnectedAsync(exception);
        }
        public Task SendConnectedUsers(string room)
        {
            var user = _connections.Values.Where(x => x.Room == room).Select(y => y.UserName);
            return Clients.Group(room).SendAsync("UserInRoom", user);
        }
    }
}