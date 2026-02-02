namespace DTOs.FooterDTOs
{
    public class GetByIdFooterDto
    {
                public int Id { get; set; }
        public string? Adress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FbUrl { get; set; }
        public string? XUrl { get; set; }
        public string? TubeUrl { get; set; }
        public string? InUrl { get; set; }
        public bool Status { get; set; }=false;
    }
}