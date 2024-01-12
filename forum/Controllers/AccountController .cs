using forum.Data;
using forum.Models;
using forum.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace forum.Controllers
{


    public class AccountController : Controller
    {


        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ForumDbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            ForumDbContext context)
        {

            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;


        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
            
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {   
           
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
       
            if (user != null)
            {
              
                // User is found check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
               
                if (passwordCheck)
                {

                    // Password Correct sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }

                // Password incorrect 
                TempData["Error"] = "Wrong credentials. Wrong Password .Please try again ";
                return View(loginViewModel);
            }

            // User not found
            TempData["Error"] = "Wrong credentials. User not found. Please try again";
            return View(loginViewModel);


        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);

        }



        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {

            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }


            var user1 = await _userManager.FindByNameAsync(registerViewModel.UserName);


            if (user1 != null)
            {
                TempData["Error"] = "This username is already in use";
                return View(registerViewModel);
            }

            var newUser = new User()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.UserName
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

                // Sign in the user after a successful registration
                await _signInManager.SignInAsync(newUser, isPersistent: true);

                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "Failed to register user";
            return View(registerViewModel);




        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }






    }

}