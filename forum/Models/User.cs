using Microsoft.AspNetCore.Identity;

namespace forum.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


public class User : IdentityUser
{
    
 
    
    public bool? Inscrit { get; set; }
    public bool? Valide { get; set; }
    public string? CheminAvatar { get; set; }
    public string? Signature { get; set; }
    public bool? Actif { get; set; } = true;
    public bool? Admin { get; set; } = false;



    public ICollection<FollowedMessages> FollowedMessages { get; set; }
    public ICollection<Post> Posts { get; set; }

    public ICollection<Post> AbonnePosts { get; set; }
    public User()
    {
    }

   

}