using System.ComponentModel.DataAnnotations.Schema;

namespace forum.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        [Key]
        public int PostID { get; set; }

        public string? Title { get; set; }

        public string Message { get; set; }
        public DateTime? DateCreationMessage { get; set; }
        public DateTime? DateCreationLastMessage { get; set; }
        public bool Sujet { get; set; }
        public string? MotCle { get; set; }

        // Navigation property for referencing posts
        public ICollection<Post>? ReferencingPosts { get; set; }

        // Navigation property for referenced posts
        public ICollection<Post>? ReferencedPosts { get; set; }

        public ICollection<User>? Users { get; set; }
        public int? ThemeId { get; set; }
        public Theme? Theme { get; set; }

        public int? ForumId { get; set; }

        public Forum Forum { get; set; }

        public ICollection<FollowedMessages>? FollowedMessages { get; set; }

        public string userID { get; set; }

        [NotMapped]
        public User CreatorUser { get; set; }
    }
}