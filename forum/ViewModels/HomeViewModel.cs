using forum.Models;
using Microsoft.AspNetCore.Mvc;

namespace forum.ViewModels
{
    public class HomeViewModel : Controller
    {
       public List<Post>? posts { get; set; }
       public List<FollowedMessages>? followedMessages { get; set; }

       public List<Post>? SearchPosts { get; set; }

       public string? searchInput { get; set; }
    }
}
