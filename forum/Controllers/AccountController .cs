



  
  using forum.Data;
using forum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Linq;


namespace forum.Controllers;


        public class AccountController : Controller
        {
            private DatabaseConnector dbConnector = new DatabaseConnector();

            public ActionResult Login()
            {
                return View();
            }

            [HttpPost]
            public ActionResult Login(User user)
            {
                if (ModelState.IsValid)
                {
                    // Perform authentication using database
                    if (AuthenticateUser(user))
                    {
                        // Authentication successful
                        return RedirectToAction("Index", "Home"); // Redirect to home or dashboard
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid credentials");
                    }
                }
                return View(user);
            }


            

            private bool AuthenticateUser(User user)
            {
                using (SqlConnection connection = dbConnector.getConnection())
                {
                    if (connection != null)
                    {
                        string query = $"SELECT COUNT(*) FROM Users WHERE Email = '{user.Email}' AND MotDePasse = '{user.MotDePasse}'";

                        SqlCommand command = new SqlCommand(query, connection);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                    else
                    {
                        // Handle null connection scenario
                        return false;
                    }
                }
            }
        }
    
