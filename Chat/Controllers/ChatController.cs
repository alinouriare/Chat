using Chat.Database;
using Chat.Hubs;
using Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Chat.Controllers
{
    [Authorize]
    [Route("Chat")]
    public class ChatController : Controller
    {
        private IHubContext<ChatHub> _chat;

        public ChatController(IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }
        [HttpPost("JoinRoom/{connectionId}/{roomId}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomId)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomId);
            return Ok();
        }
        [HttpPost("LeaveRoom/{connectionId}/{roomId}")]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomId)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomId);
            return Ok();
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(int roomId, string message, [FromServices] AppDbContext ctx)
        {
            var Message = new Messages
            {
                ChatId=roomId,
                Text = message,
                Name = User.Identity.Name,
                Timestap = DateTime.Now
            };
            ctx.Messages.Add(Message);
            await ctx.SaveChangesAsync();

            await _chat.Clients.Group(roomId.ToString()).SendAsync("ReciveMessage", new
            {
                
                text = Message.Text,
                name = Message.Name,
                timestap = Message.Timestap.ToString("dd/MM/yyyy hh:mm:ss")
            });
            return Ok();
        }
    }
}
