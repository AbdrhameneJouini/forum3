using forum.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace forum.ViewModels
{
    public class ForumPostsViewModel
    {
        public int ForumId { get; set; }
        public string ForumTitle { get; set; }
        public List<Post> Posts { get; set; }
        
    }
}
