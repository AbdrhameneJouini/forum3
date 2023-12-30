using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace forum.Models
{


    [Index(nameof(Titre), IsUnique = true)]
    public class Theme
    {
        [Key]
        public int ThemeID { get; set; }


        [Required]
        [MaxLength(255)] 
        public string Titre { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Forum> Forums { get; set; }

        public Theme()
        {
            // Initialize collections if needed
            Posts = new List<Post>();
            Forums = new List<Forum>();
        }

        public Theme(int themeID, string titre)
        {
            ThemeID = themeID;
            Titre = titre;

            Posts = new List<Post>();
            Forums = new List<Forum>();
        }
    }
}