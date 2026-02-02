namespace DTOs.RoomTypeDTOs
{
    public class UpdateRoomTypeDto
    {
         public int Id { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }=false;

    }
}