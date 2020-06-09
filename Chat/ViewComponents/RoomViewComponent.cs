using Chat.Database;
using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chat.ViewComponents
{
    public class RoomViewComponent:ViewComponent
    {
        private AppDbContext _ctx;

        public RoomViewComponent(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IViewComponentResult Invoke()
        {
            var userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var chat = _ctx.ChatUsers.Include(x=>x.Chat)
                .Where(c=>c.UserId == userid && c.Chat.Type==ChatType.Room)
                .Select(x=>x.Chat)
                .ToList();
            return View(chat);
        }
    }
}
