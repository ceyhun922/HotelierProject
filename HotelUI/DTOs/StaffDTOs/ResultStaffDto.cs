namespace DTOs.StaffDTOs
{
    public class ResultStaffDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? FbUrl { get; set; }
        public string? XUrl { get; set; }
        public string? InstaUrl { get; set; }
        public bool Status { get; set; } = false;
    }
}