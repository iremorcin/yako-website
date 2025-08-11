using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yako.Infrastructure;
using Yako.Infrastructure.Entities;
using Yako.Models;
using Yako.UI.Models;

namespace Yako.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _dataContext;

        public HomeController(ILogger<HomeController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(BusinessSearchModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.Location) && string.IsNullOrWhiteSpace(model.Category))
            {
                ViewData["SearchResults"] = new List<Business>(); 
                return View(model);
            }

            var query = _dataContext.Businesses
                .Include(b => b.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(model.Location))
            {
                query = query.Where(b => b.Location.Contains(model.Location));
            }

            if (!string.IsNullOrWhiteSpace(model.Category))
            {
                query = query.Where(b => b.Category.Name.Contains(model.Category));
            }

            var result = await query.ToListAsync();
            ViewData["SearchResults"] = result;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
