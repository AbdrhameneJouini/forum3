﻿namespace forum.Models
{
    public class FollowedMessages
    {
        public bool Lu { get; set; } = false;
        public bool Archive { get; set; } = false;
  
        public DateTime CreatioDateTime { get; set; }

        public int postId { get; set; }
        public Post Post { get; set; }
        public String userId { get; set; }
        public User User { get; set; }
    }
}
