namespace forum.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


    public class User
    {

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
}

