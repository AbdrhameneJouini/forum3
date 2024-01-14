using forum.Models;
using forum.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ForumDbContext _context;

        public HomeController(ILogger<HomeController> logger, ForumDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Get the newest posts that are subjects
            var newestSubjects = _context.Posts
                .Where(p => p.Sujet)
                .OrderByDescending(p => p.DateCreationLastMessage)
                .Take(20)  // Assuming you want to display the top 5 newest subjects
                .ToList();

            foreach (var post in newestSubjects)
            {
                // Search for the user associated with the post
                var user = _context.Users.FirstOrDefault(u => u.Id == post.userID);

                // Assign the user to the CreatorUser property
                post.CreatorUser = user;
            }


            return View(newestSubjects);
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