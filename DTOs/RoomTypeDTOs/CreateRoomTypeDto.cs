namespace DTOs.RoomTypeDTOs
{
    public class CreateRoomTypeDto
    {
        public string Type { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }=false;

    }
}