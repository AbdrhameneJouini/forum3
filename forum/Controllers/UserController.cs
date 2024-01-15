using forum.Models;
using forum.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace forum.Controllers
{
    public class UserController : Controller
    {
        private readonly ForumDbContext _context;
        private readonly UserManager<User> _userManager;



        public UserController( ForumDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }




        public ActionResult Index()
        {


            var users = _context.Users
                .ToList();


            var userViewModel = new UserViewModel()
            {
                users = users,


            };
            return View(userViewModel);
        }


        public ActionResult Validate(string id)
        {
            // Find the user by id
            var user = _context.Users.SingleOrDefault(u => u.Id == id);

            if (user != null)
            {
                user.Valide = true;

                _context.SaveChanges();
            }
            else
            {
                
                return RedirectToAction("Index", "User");
            }

            // Redirect to the Users index
            return RedirectToAction("Index", "User");
        }



    }
}
