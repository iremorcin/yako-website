using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yako.Infrastructure;
using Yako.Infrastructure.Entities;

namespace Yako.UI.Controllers
{
    public class BusinessDetailController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<AppUser> _userManager;

        public BusinessDetailController(DataContext dataContext, UserManager<AppUser> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }

        public IActionResult Index(Guid id)
        {
            var business = _dataContext.Businesses.Include(b => b.Category).FirstOrDefault(b => b.Id == id);
            if (business == null)
                return NotFound();

            return View(business);
        }

        [HttpPost]
        public async Task<IActionResult> RateBusiness(Guid BusinessId, int Rating)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Error"] = "Lütfen puanlama yapabilmek için giriş yapın.";
                return RedirectToAction("Index", new { id = BusinessId });
            }

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var existingRating = await _dataContext.BusinessRatings
                .FirstOrDefaultAsync(r => r.BusinessId == BusinessId && r.UserId == userId);

            if (existingRating != null)
            {
                existingRating.Rating = Rating;
            }
            else
            {
                var newRating = new BusinessRating
                {
                    Id = Guid.NewGuid(),
                    BusinessId = BusinessId,
                    UserId = userId,
                    Rating = Rating
                };
                _dataContext.BusinessRatings.Add(newRating);
            }

            await _dataContext.SaveChangesAsync();

            var avg = await _dataContext.BusinessRatings
                .Where(x => x.BusinessId == BusinessId)
                .AverageAsync(x => (double?)x.Rating);

            var business = await _dataContext.Businesses.FindAsync(BusinessId);
            business.AverageRating = avg;
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index", new { id = BusinessId });
        }
    }
}
