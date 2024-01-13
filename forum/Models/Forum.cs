namespace forum.Models;

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


        public class Forum
        {
        [Key]
        public int ForumID { get; set; }
        public string Titre { get; set; }
        public DateTime DateCreation { get; set; }
        public ICollection<Theme> Themes { get;  set; }

        [NotMapped]
        public IEnumerable<SelectListItem> ThemesList
        {
            get
            {
                return Themes.Select(t => new SelectListItem { Value = t.ThemeID.ToString(), Text = t.Titre });
            }
        }

}

