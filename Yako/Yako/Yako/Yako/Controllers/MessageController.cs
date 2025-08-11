using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yako.Infrastructure.Entities;
using Yako.Infrastructure;

namespace Yako.UI.Controllers
{
    public class MessageController : Controller
    {
        private readonly DataContext _context;

        public MessageController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail))
            {
                return View(new List<MessageToo>());
            }

            var messages = _context.MessageToos
                .Where(m => m.Email == userEmail)
                .ToList();

            return View(messages);
        }
    }
}
