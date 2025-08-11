using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yako.Infrastructure;
using Yako.Infrastructure.Entities;

namespace Yako.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminContactController : Controller
    {
        private readonly DataContext _dataContext;

        public AdminContactController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var messages = _dataContext.Messages.ToList();
            return View(messages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reply(Guid OriginalId, string Email, string Content)
        {
            var original = _dataContext.Messages.FirstOrDefault(x => x.Id == OriginalId);

            if (original != null)
            {
                var reply = new MessageToo
                {
                    Id = Guid.NewGuid(),
                    FullName = original.FullName, 
                    Email = Email,
                    Content = Content
                };

                _dataContext.MessageToos.Add(reply);
                _dataContext.SaveChanges();
            }

            TempData["Success"] = "Yanıt başarıyla gönderildi.";
            return RedirectToAction("Index");
        }
    }
}
