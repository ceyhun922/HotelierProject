namespace DTOs.ContactDTOs
{
    public class GetByIdContactDto
    {
         public int Id { get; set; }
        public string? MapLocation { get; set; }
        public string? MailBooking { get; set; }
        public string? MailGeneral { get; set; }
        public string? MailTechnical { get; set; }
        public bool Status { get; set; }=false;
    }
}