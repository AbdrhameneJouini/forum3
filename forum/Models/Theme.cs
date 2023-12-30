namespace forum.Models
{
    public class Theme
    {
        public int ThemeID { get; set; }
        public string Titre { get; set; }
        public List<int> Forums { get; internal set; }
    }
}
