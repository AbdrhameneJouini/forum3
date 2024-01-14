using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace forum.ViewModels
{
    public class PostViewModel
    {

        public string? PostId { get; set; }
        public string? PostTitle { get; set; }


        [Required]
        public string PostMessage { get; set; }

        public string? MotsCle { get; set; }

        public int? userID { get; set; }

        public bool? sujet { get; set; }

        public int? forumID { get; set; }

        



    }
}
