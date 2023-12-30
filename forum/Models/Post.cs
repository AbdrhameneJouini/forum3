namespace forum.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class Post
{
    public int PostID { get; set; }
    public string Message { get; set; }
    public DateTime DateCreationMessage { get; set; }
    public bool Sujet { get; set; }
    public string MotCle { get; set; }
    public int FilID { get; set; }
}
