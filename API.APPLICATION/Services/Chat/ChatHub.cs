using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using API.DOMAIN.DTOs.Chat;
using BaseCommon.Common.ClaimUser;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BaseCommon.Common.Enum;

namespace API.APPLICATION
{
    //[Authorize]
    public class ChatHub : Hub
    {
        private readonly string _botUser;
        private readonly IDictionary<string, UserConnection> _connections;
        private readonly IUserSessionInfo _userSessionInfo;
        public ChatHub(IDictionary<string, UserConnection> connections, IUserSessionInfo userSessionInfo)
        {
            _botUser = "MyChat Bot";
            _connections = connections;
            _userSessionInfo = userSessionInfo;
        }
        protected virtual string GetUserId(ClaimsPrincipal user)
        {
            Claim userIdClaim = user?.Claims.SingleOrDefault(x => x.Type == ClaimsTypeName.USER_ID);

            if (userIdClaim == null || string.IsNullOrWhiteSpace(userIdClaim.Value)) return string.Empty;

            return userIdClaim.Value;
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
          
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                
                _connections.Remove(Context.ConnectionId);
                Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", _botUser, $"{userConnection.UserName} has left");
                SendUsersConnected(userConnection.Room);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinRoom(UserConnection userConnection)
        {
            var user = _userSessionInfo.UserName;
            
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);

            _connections[Context.ConnectionId] = userConnection;

            await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", _botUser, $"{userConnection.UserName} has joined {userConnection.Room}");

            await SendUsersConnected(userConnection.Room);
        }

        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", userConnection.UserName, message);
            }
        }

        public Task SendUsersConnected(string room)
        {
            var users = _connections.Values
                .Where(c => c.Room == room)
                .Select(c => c.UserName);

            return Clients.Group(room).SendAsync("UsersInRoom", users);
        }

    }
}