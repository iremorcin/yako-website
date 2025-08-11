using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Yako.Infrastructure;
using Yako.Infrastructure.Entities;
using Yako.UI.Areas.Admin.Models;

namespace Yako.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BusinessController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public BusinessController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var businesses = await _context.Businesses.Include(b => b.Category).ToListAsync();
            return View(businesses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BusinessCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(model);
            }

            var business = new Business
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Address = model.Address,
                Description = model.Description,
                Location = model.Location,
                MapUrl = model.MapUrl,
                CategoryId = model.CategoryId,
                AverageRating = 0
            };

            if (model.Image != null && model.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "img");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }

                business.ImageUrl = "/img/" + uniqueFileName;
            }

            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var business = await _context.Businesses.FindAsync(id);
            if (business == null)
                return NotFound();

            var dto = new BusinessCreateDto
            {
                Title = business.Title,
                Address = business.Address,
                Description = business.Description,
                Location = business.Location,
                MapUrl = business.MapUrl,
                CategoryId = business.CategoryId
            };

            ViewBag.Categories = _context.Categories.ToList();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, BusinessCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(model);
            }

            var business = await _context.Businesses.FindAsync(id);
            if (business == null)
                return NotFound();

            business.Title = model.Title;
            business.Address = model.Address;
            business.Description = model.Description;
            business.Location = model.Location;
            business.MapUrl = model.MapUrl;
            business.CategoryId = model.CategoryId;

            if (model.Image != null && model.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "img");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }

                business.ImageUrl = "/img/" + uniqueFileName;
            }

            _context.Businesses.Update(business);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var business = await _context.Businesses.FindAsync(id);
            if (business == null)
                return NotFound();

            _context.Businesses.Remove(business);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
