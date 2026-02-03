namespace DTOs.TeamDtos
{
    public class GetByIdTeamDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? Sosial1 { get; set; }
        public string? Sosial2 { get; set; }
        public string? Sosial3 { get; set; }
        public bool Status { get; set; }
    }
}