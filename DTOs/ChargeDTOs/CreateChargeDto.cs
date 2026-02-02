namespace DTOs.ChargeDTOs
{
    public class CreateChargeDto
    {
        public string? Icon { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }=false;
    }
}