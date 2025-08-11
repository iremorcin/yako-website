using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yako.Infrastructure;
using Yako.Infrastructure.Entities;
using Yako.UI.Models;

namespace Yako.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly DataContext _dataContext;

        public ContactController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                var contact = new Message
                {
                    Id = Guid.NewGuid(),
                    FullName = model.Name,
                    Email = model.Email,
                    Content = model.Message,
                };
                
                _dataContext.Messages.Add(contact);
                await _dataContext.SaveChangesAsync();
                TempData["Success"] = "Mesajınız Başarı İle Göderildi";
                return RedirectToAction("Index"); 
            }

            return View(model);
        }
    }
}
