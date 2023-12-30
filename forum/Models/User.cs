namespace forum.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


    public class User
    {
    [Key]
    public int UserID { get; set; }
    public string Pseudonyme { get; set; }
    public string MotDePasse { get; set; }
    public string Email { get; set; }
    public bool Inscrit { get; set; }
    public bool Valide { get; set; }
    public string CheminAvatar { get; set; }
    public string Signature { get; set; }
    public bool Actif { get; set; } = true;
    public bool Admin { get; set; } = false;


    public ICollection<FollowedMessages> FollowedMessages { get; set; }
    public ICollection<Post> Posts { get; set; }
    public User()
    {
    }

    // Constructor with parameters to set basic properties
    public User(string pseudonyme, string motDePasse, string email)
    {
        Pseudonyme = pseudonyme;
        MotDePasse = motDePasse;
        Email = email;
        // Set default values for other properties if needed
    }

    // Constructor with parameters to set all properties including UserID
  
    public User(int userID, string pseudonyme, string motDePasse, string email, bool inscrit, bool valide, string cheminAvatar, string signature, bool actif, bool admin)
    {
        UserID = userID;
        Pseudonyme = pseudonyme;
        MotDePasse = motDePasse;
        Email = email;
        Inscrit = inscrit;
        Valide = valide;
        CheminAvatar = cheminAvatar;
        Signature = signature;
        Actif = actif;
        Admin = admin;
    }
    
}


