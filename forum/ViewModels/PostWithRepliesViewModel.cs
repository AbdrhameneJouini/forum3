using forum.Models;

namespace forum.ViewModels
{
    public class PostWithRepliesViewModel
    {

        public Post MainPost { get; set; }
        public List<Post> Replies { get; set; }

      
    }
}
