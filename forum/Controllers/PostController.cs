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




        public IActionResult Index(int? id)
        {
            var post = _context.Posts
                .Include(p => p.Users)  // Ensure Users are loaded
                .Include(p => p.ReferencedPosts)  // Ensure ReferencedPosts are loaded
                .FirstOrDefault(p => p.Sujet && p.PostID == id);

            if (post != null)
            {
                var creator = _context.Users.FirstOrDefault(u => u.Id == post.userID);

                if (creator != null)
                {
                    post.CreatorUser = creator;
                }

                // Filter out only the replies (posts where Sujet is false)
                var replies = post.ReferencedPosts.Where(p => !p.Sujet).ToList();


                // Loop through the ReferencedPosts to set the CreatorUser for each reply
                foreach (var reply in replies)
                {
                    var replyCreator = _context.Users.FirstOrDefault(u => u.Id == reply.userID);
                    if (replyCreator != null)
                    {
                        reply.CreatorUser = replyCreator;
                    }


                }
                
                // Pass the main post and its replies to the view

                return View(new PostWithRepliesViewModel
                {
                    MainPost = post,
                    Replies = replies
                }
                
                );
            }

            return NotFound();
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






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reply(int? id)
        {
            var post = _context.Posts
                .Include(p => p.Users)
                .Include(p => p.ReferencedPosts)
                .FirstOrDefault(p => p.Sujet && p.PostID == id);

            var userId = _userManager.GetUserId(HttpContext.User);
            string message = HttpContext.Request.Form["message"];

            if (post != null)
            {
                // Check if the current user is not already added to the post's Users
                if (post.Users == null)
                {
                    post.Users = new List<User>();
                }

                var currentUser = post.Users.FirstOrDefault(u => u.Id == userId);
                if (currentUser == null)
                {
                    // Add the current user to the post's Users
                    var user = await _userManager.FindByIdAsync(userId);
                    post.Users.Add(user);
                }

                // Create a new reply post
                var replyPost = new Post
                {
                    Message = message,
                    DateCreationMessage = DateTime.Now,
                    DateCreationLastMessage = DateTime.Now,
                    Sujet = false,
                    ForumId = post.ForumId,
                    userID = userId
                };

                // Add the reply post to the post's ReferencedPosts
                if (post.ReferencedPosts == null)
                {
                    post.ReferencedPosts = new List<Post>();
                }
                post.ReferencedPosts.Add(replyPost);

                // Update DateCreationLastMessage of the main post
                post.DateCreationLastMessage = DateTime.Now;

                // Save changes to the database
                await _context.SaveChangesAsync();

              

                return RedirectToAction(nameof(Index), new { id = post.PostID });
            }

            return NotFound();
        }









    }
}


