using Microsoft.AspNetCore.Mvc;
using forum.Models; 

namespace forum.Controllers
{
    public class FollowedMessagesController : Controller
    {
        private readonly ForumDbContext _context; 

        public FollowedMessagesController(ForumDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Archive(string userId, int postId, DateTime creationDate)
        {
            // Fetch the record from the database based on the provided parameters
            var message = _context.FollowedMessages
                .FirstOrDefault(m => m.userId == userId && m.postId == postId &&
                 m.CreatioDateTime.Year == creationDate.Year &&
                m.CreatioDateTime.Month == creationDate.Month &&
                m.CreatioDateTime.Day == creationDate.Day &&
                m.CreatioDateTime.Hour == creationDate.Hour &&
                m.CreatioDateTime.Minute == creationDate.Minute &&
                m.CreatioDateTime.Second == creationDate.Second     );


            if (message != null)
            {
                message.Archive = true;
                message.Lu = true;

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}