using forum.Models;
using forum.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ForumDbContext _context;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, ForumDbContext context, UserManager<User> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        // Assuming PostModel is the model class for your posts
        public IActionResult Index()
        {
            var loggedUserId = _userManager.GetUserId(HttpContext.User);

            // Get the newest posts that are subjects
            var newestSubjects = _context.Posts
                .Include(p => p.AbonneUsers)
                .Where(p => p.Sujet)
                .OrderByDescending(p => p.DateCreationLastMessage)
                .Take(20)  // Assuming you want to display the top 20 newest subjects
                .ToList();

            foreach (var post in newestSubjects)
            {
                // Search for the user associated with the post
                var user = _context.Users.FirstOrDefault(u => u.Id == post.userID);

                // Assign the user to the CreatorUser property
                post.CreatorUser = user;

                // Check if the logged-in user is in AbonneUsers for this post
                if (post.AbonneUsers != null)
                {
                    post.isInFavorite = post.AbonneUsers.Any(u => u.Id == loggedUserId);
                }
                else
                {
                    post.isInFavorite = false;
                }
            }

            // Assuming FollowedMessages is the entity for the followed messages
            var newestFollowedMessages = _context.FollowedMessages
                .Include(fm => fm.User)
                .Include(fm => fm.Post )
                .ThenInclude(p => p.ReferencedPosts)
                .Where(fm => fm.userId == loggedUserId && fm.Lu == false && fm.Archive == false)
                .OrderByDescending(fm => fm.CreatioDateTime)
                .Take(5)
                .ToList();


            var homeViewModel = new HomeViewModel()
            {
                posts = newestSubjects,
                followedMessages = newestFollowedMessages
            };

            return View(homeViewModel);
        }

        public IActionResult Search()
        {

            var loggedUserId = _userManager.GetUserId(HttpContext.User);

            string searchText = HttpContext.Request.Form["search"];

            var searchResults = _context.Posts
                .Where(p =>   ( p.Title.Contains(searchText)    || p.MotCle.Contains(searchText)   )     && p.Sujet)
                .Include(p => p.AbonneUsers)
                .ToList();

            foreach (var post in searchResults)
            {
                // Search for the user associated with the post
                var user = _context.Users.FirstOrDefault(u => u.Id == post.userID);

                // Assign the user to the CreatorUser property
                post.CreatorUser = user;

                // Check if the logged-in user is in AbonneUsers for this post
                if (post.AbonneUsers != null)
                {
                    post.isInFavorite = post.AbonneUsers.Any(u => u.Id == loggedUserId);
                }
                else
                {
                    post.isInFavorite = false;
                }
            }



            var newestFollowedMessages = _context.FollowedMessages
                .Include(fm => fm.User)
                .Include(fm => fm.Post)
                .ThenInclude(p => p.ReferencedPosts)
                .Where(fm => fm.userId == loggedUserId && fm.Lu == false && fm.Archive == false)
                .OrderByDescending(fm => fm.CreatioDateTime)
                .Take(5)
                .ToList();
            var homeViewModel = new HomeViewModel()
            {
                searchInput = searchText,
                SearchPosts = searchResults,
                followedMessages = newestFollowedMessages

            };
            return View("Index", homeViewModel);
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