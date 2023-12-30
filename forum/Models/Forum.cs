namespace forum.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    public class Forum
    {
    public int ForumID { get; set; }
    public string Titre { get; set; }
    public DateTime DateCreation { get; set; }
    public List<Theme> Themes { get; internal set; }
}

