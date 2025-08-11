using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yako.Infrastructure;
using Yako.UI.Models;

namespace Yako.UI.Controllers
{
    public class BusinessFindController : Controller
    {
        private readonly DataContext _dataContext;

        public BusinessFindController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allBusinesses = await _dataContext.Businesses
                .Include(b => b.Category)
                .Select(b => new BusinessModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    AverageRating = b.AverageRating,
                    ImageUrl = b.ImageUrl,
                    MapUrl = b.MapUrl,
                }).ToListAsync();

            var model = new BusinessIndexViewModel
            {
                Businesses = allBusinesses
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BusinessIndexViewModel model)
        {
            var query = _dataContext.Businesses.Include(b => b.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(model.Location))
                query = query.Where(b => b.Location.Contains(model.Location));

            if (!string.IsNullOrWhiteSpace(model.Category))
                query = query.Where(b => b.Category.Name.Contains(model.Category));

            model.Businesses = await query.Select(b => new BusinessModel
            {
                Id=b.Id,
                Title = b.Title,
                Description = b.Description,
                AverageRating = b.AverageRating,
                ImageUrl = b.ImageUrl,
                MapUrl = b.MapUrl,
            }).ToListAsync();

            return View(model);
        }
    }
}
