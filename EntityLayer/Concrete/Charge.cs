namespace EntityLayer.Concrete
{
    public class Charge
    {
        public int Id { get; set; }
        public string? Icon { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }=false;

    }
}