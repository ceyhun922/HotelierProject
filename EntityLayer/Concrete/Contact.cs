namespace EntityLayer.Concrete
{
    public class Contact
    {
        public int Id { get; set; }
        public string? MapLocation { get; set; }
        public string? MailBooking { get; set; }
        public string? MailGeneral { get; set; }
        public string? MailTechnical { get; set; }
        public string? Adress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FbUrl { get; set; }
        public string? XUrl { get; set; }
        public string? TubeUrl { get; set; }
        public string? InUrl { get; set; }
        public bool Status { get; set; }=false;
        
    }
}