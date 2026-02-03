namespace DTOs.MessageDTOs
{
    public class UpdateMessageDto
    {
          public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Msg { get; set; }
        public bool Status { get; set; }=false;
    }
}