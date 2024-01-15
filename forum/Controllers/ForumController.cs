using forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using forum.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly ForumDbContext _context;
        private readonly UserManager<User> _userManager;

        public ForumController(ForumDbContext context,UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Forum
        public async Task<IActionResult> Index()
        {
            var forumsWithThemes = await _context.Forums
                .Include(f => f.Themes) 
                .ThenInclude(t => t.Forums)
                .ThenInclude(tf => tf.Themes) 
                .ToListAsync();

            return View(forumsWithThemes);
        }


        // GET: Forum/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }

            var forum = await _context.Forums
                .Include(f => f.Themes)
                .FirstOrDefaultAsync(m => m.ForumID == id);

            if (forum == null)
            {
                return NotFound();
            }

            return View(forum);
        }

        // GET: Forum/Create
        public IActionResult Create()
        {
            var model = new ForumViewModel
            {
                ThemesList = _context.Themes
                    .Select(t => new SelectListItem { Value = t.ThemeID.ToString(), Text = t.Titre })
                    .ToList()
            };

            return View(model);
        }


        // POST: Forum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titre,DateCreation,SelectedThemes")] ForumViewModel forumViewModel)
        {
            forumViewModel.ThemesList = _context.Themes
                .Select(t => new SelectListItem { Value = t.ThemeID.ToString(), Text = t.Titre })
                .ToList();

            if (ModelState.IsValid)
            {
                // Convert ForumViewModel to Forum entity
                var forum = new Forum
                {
                    Titre = forumViewModel.Titre,
                    DateCreation = DateTime.Now,
                    Themes = _context.Themes
                        .Where(t => forumViewModel.SelectedThemes.Contains(t.ThemeID.ToString()))
                        .ToList()
                };

                // Add the forum and associated themes to the database
                _context.Add(forum);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // If ModelState is not valid, repopulate the ThemesList property
            forumViewModel.ThemesList = _context.Themes
                .Select(t => new SelectListItem { Value = t.ThemeID.ToString(), Text = t.Titre });

            return View(forumViewModel);
        }




        // GET: Forum/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }

            var forum = await _context.Forums
                .Include(f => f.Themes)
                .FirstOrDefaultAsync(m => m.ForumID == id);

            if (forum == null)
            {
                return NotFound();
            }

            var forumViewModel = new ForumViewModel
            {
                ForumID = forum.ForumID,
                Titre = forum.Titre,
                SelectedThemes = forum.Themes.Select(t => t.ThemeID.ToString()).ToList(),
                ThemesList = _context.Themes
                    .Select(t => new SelectListItem { Value = t.ThemeID.ToString(), Text = t.Titre })
                    .ToList()
            };

            return View(forumViewModel);
        }

        // POST: Forum/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ForumID,Titre,SelectedThemes")] ForumViewModel forumViewModel)
        {
            if (ModelState.IsValid)
            {
                var forum = await _context.Forums
                    .Include(f => f.Themes)
                    .FirstOrDefaultAsync(m => m.ForumID == id);

                if (forum == null)
                {
                    return NotFound();
                }

                forum.Titre = forumViewModel.Titre;

                forum.Themes = _context.Themes
                    .Where(t => forumViewModel.SelectedThemes.Contains(t.ThemeID.ToString()))
                    .ToList();

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumExists(forum.ForumID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            forumViewModel.ThemesList = _context.Themes
                .Select(t => new SelectListItem { Value = t.ThemeID.ToString(), Text = t.Titre })
                .ToList();

            return View(forumViewModel);
        }




        // GET: Forum/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }

            var forum = await _context.Forums
                .Include(f => f.Themes)
                .FirstOrDefaultAsync(m => m.ForumID == id);

            if (forum == null)
            {
                return NotFound();
            }

            return View(forum);
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Forums == null)
            {
                return Problem("Entity set 'ForumDbContext.Forums' is null.");
            }

            var forum = await _context.Forums.FindAsync(id);
            if (forum != null)
            {
                _context.Forums.Remove(forum);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ForumExists(int id)
        {
          return (_context.Forums?.Any(e => e.ForumID == id)).GetValueOrDefault();
        }





        // Add this method to your ForumController
        // GET: Forum/Posts/5
        public async Task<IActionResult> Posts(int? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }

            var forum = await _context.Forums
                .Include(f => f.Posts)
                .ThenInclude(p => p.Theme) // Include theme for each post
                .FirstOrDefaultAsync(m => m.ForumID == id);

            if (forum == null)
            {
                return NotFound();
            }


            var loggedUserId = _userManager.GetUserId(HttpContext.User);


            var sujetPosts = _context.Posts
                .Include(p => p.AbonneUsers)
                .Where(p => p.Sujet && p.ForumId == id)
                .OrderByDescending(p => p.DateCreationLastMessage)
                .ToList();

            foreach (var post in sujetPosts)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == post.userID);

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

            var viewModel = new ForumPostsViewModel
            {
                ForumId = forum.ForumID,
                ForumTitle = forum.Titre,
                Posts = sujetPosts,
             
            };

            return View("Posts/Posts", viewModel);
        }

            
        

    }
}
