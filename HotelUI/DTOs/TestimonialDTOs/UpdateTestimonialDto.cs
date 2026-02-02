namespace DTOs.TestimonialDTOs
{
    public class UpdateTestimonialDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Title { get; set; }
        public string? Message {get;set;}
        public bool Status { get; set; }=false;
    }    
}
