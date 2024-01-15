using forum.Models;

namespace forum.ViewModels
{
    public class PostWithRepliesViewModel
    {

        public Post MainPost { get; set; }
        public List<Post> Replies { get; set; }


        public Dictionary<int, bool> CanEditReplies { get; set; }

        public  bool CanEditMainPost { get; set; }



    }
}
