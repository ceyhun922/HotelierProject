namespace Hotelier.EntityLayer.Concrete
{
    public class Slider
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Title1 { get; set; }
        public string? Title2 { get; set; }
        public bool Status { get; set; }=false;
    }
}