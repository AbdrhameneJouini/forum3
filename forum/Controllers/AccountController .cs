using forum.Data;
using forum.Models;
using forum.ViewModels;
using Microsoft.AspNetCore.Hosting;
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
                UserName = registerViewModel.UserName,
                CheminAvatar = "/uploads/Default_Avatar.jpg"
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



        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            
            var user = _context.Users.FirstOrDefault(u => u.Id == _userManager.GetUserId(HttpContext.User));
            var ProfileViewModel = new ProfileViewModel()
            {
                UserName = user.UserName,
                Signature = user.Signature,
                Email = user.Email,
                id = user.Id,
                imageURL = user.CheminAvatar


            };
            return View(ProfileViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileViewModel model, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                // Update user properties based on the form inputs
                user.UserName = model.UserName;
                user.Signature = model.Signature;
                user.Email = model.Email;

                // Change password only if NewPassword is provided
                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, null, model.NewPassword);
                    if (!changePasswordResult.Succeeded)
                    {
                        // Handle password change failure (e.g., invalid password)
                        foreach (var error in changePasswordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        return View("Profile", model);
                    }
                }

                if (model.AvatarImage != null)
                {
                    // Ensure the "uploads" folder exists
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AvatarImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.AvatarImage.CopyToAsync(fileStream);
                    }

                    // Update the model with the image URL
                    model.imageURL = "/uploads/" + uniqueFileName;
                    user.CheminAvatar = model.imageURL;
                }

                // Save changes to the user
                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Handle update failure (e.g., validation errors)
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View("Profile", model);
                }
            }

            // ModelState is invalid; return to the form with validation errors
            return View("Profile", model);
        }






    }

}