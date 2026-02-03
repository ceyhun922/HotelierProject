namespace DTOs.TeamDTOs
{
    public class CreateTeamDto
    {
        public string? FullName { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? Sosial1 { get; set; }
        public string? Sosial2 { get; set; }
        public string? Sosial3 { get; set; }
        public bool Status { get; set; }
    }
}