using Chat.Database;
using Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Chat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private AppDbContext _ctx;

        public HomeController(AppDbContext ctx)
        {
            _ctx = ctx;

        }
     
        public IActionResult Index()
        {
            var chat = _ctx.Chats.Include(x=>x.Users)
                .Where(x => !x.Users.Any(c => c.UserId == User.FindFirst(ClaimTypes.NameIdentifier)
                .Value))
                .ToList();
            return View(chat);
        }
        public IActionResult Private()
        {

            var chat = _ctx.Chats.Include(x => x.Users)
                .ThenInclude(p=>p.User)
                .Where(c => c.Type == ChatType.Private &&
                kl,km, c.Users.Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
                .ToList();
            return View(chat);
        }

        public IActionResult Find()
        {

            var user= _ctx.Users.Where(x => x.Id != User.FindFirst(ClaimTypes.NameIdentifier).Value).ToList();
            return View(user);
        }

        public async Task<IActionResult>  CreateRoomPrivate(string userId)
        {
            var chat = new Models.Chat
            {
                Type=ChatType.Private,
                
            };

            chat.Users.Add(new ChatUser { UserId=userId});
            chat.Users.Add(new ChatUser { UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value });

            _ctx.Chats.Add(chat);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Chat", new { id = chat.Id });

        }

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");

            }
            var chat = _ctx.Chats
                 .Include(x => x.Messages)
                 .FirstOrDefault(c => c.Id == id);
            return View(chat);
        }
    
        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var Message = new Messages
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                Timestap = DateTime.Now
            };

            _ctx.Messages.Add(Message);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Chat",new { id=chatId});
        }
        public async Task<IActionResult> CreateRoom(string name)

        {
            var chat = new Models.Chat
            {
                Name = name,
                Type = ChatType.Room

            };
            chat.Users.Add(new ChatUser
            {
                Role=UserRole.Admin,
                UserId=User.FindFirst(ClaimTypes.NameIdentifier).Value
            });

            _ctx.Chats.Add(chat);

            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult>   JoinRoom(int id)
        {

            var chatUser = new ChatUser
            {
                ChatId=id,
                Role = UserRole.Member,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            };

             _ctx.ChatUsers.Add(chatUser);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Chat","Home",new { id=id});

        }
    }
}
