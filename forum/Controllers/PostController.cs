using forum.Models;
using forum.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace forum.Controllers
{
    public class PostController : Controller
    {
        private readonly ForumDbContext _context;
        private readonly UserManager<User> _userManager;
        public PostController(ForumDbContext context, UserManager<User>  userManager)
        {
            _context = context;
            _userManager = userManager;
        }




        public IActionResult Index()
        {
            return View();
        }

    





        // POST: Forum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {

            var userId = _userManager.GetUserId(HttpContext.User);
         

                string title = HttpContext.Request.Form["title"];
                string post = HttpContext.Request.Form["post"];
                string keywords = HttpContext.Request.Form["keywords"];
                bool isSubjet = Convert.ToBoolean(HttpContext.Request.Form["isSubjet"]);
                int forumId = Convert.ToInt32(HttpContext.Request.Form["forumId"]);




            if (ModelState.IsValid)
            {


                var newPost = new Post
                {
                    Title = title,
                    Message = post,
                    MotCle = keywords,
                    Sujet = isSubjet,
                    DateCreationMessage = DateTime.Now,
                    DateCreationLastMessage = DateTime.Now,
                    ForumId = forumId,
                    userID = userId,
                    Users = _context.Users
                        .Where(u => u.Id == userId )
                        .ToList()
                    
                   
                };
                Console.WriteLine("HI");

                _context.Posts.Add(newPost);

                await _context.SaveChangesAsync();
                

            }



            return RedirectToAction("Posts", "Forum", new { id = forumId });

        }
    }
}
