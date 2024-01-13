using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using forum.Models;

namespace forum.ViewModels
{
    public class ForumViewModel
    {

        public ForumViewModel()
        {
            // Initialize ThemesList to an empty list if it is null
            ThemesList = ThemesList ?? new List<SelectListItem>();
            ForumID = ForumID != 0 ? ForumID : -1;
        }


        [Required]
        public string Titre { get; set; }


      
        public int ForumID { get; set; }

        [Required]
        public List<string> SelectedThemes { get; set; }


        public IEnumerable<SelectListItem>? ThemesList { get; set; }

    }

}
