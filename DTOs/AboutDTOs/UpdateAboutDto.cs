namespace DTOs.AboutDTOs
{
    public class UpdateAboutDto
    {
         public int Id { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; } = false;
    }
}