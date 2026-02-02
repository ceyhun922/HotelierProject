namespace DTOs.ContactDTOs
{
    public class CreateContactDto
    {
        public string? MapLocation { get; set; }
        public string? MailBooking { get; set; }
        public string? MailGeneral { get; set; }
        public string? MailTechnical { get; set; }
        public bool Status { get; set; }=false;
    }
}