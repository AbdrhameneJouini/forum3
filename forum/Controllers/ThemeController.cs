using forum.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using forum.Models;

namespace forum.Controllers
{
    public class ThemeController : Controller
    {
        private readonly forumContext _context;

        public ThemeController(forumContext context)
        {
            _context = context;
        }

        // Add this action method to handle the logic for creating a new theme
        [HttpPost]
        public IActionResult CreateTheme(Theme theme)
        {
            Console.WriteLine("Hello in the create theme function");
            if (ModelState.IsValid)
            {
                _context.Add(theme);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home"); // Redirect to the home page
            }
            return View(theme);
        }

    }
}