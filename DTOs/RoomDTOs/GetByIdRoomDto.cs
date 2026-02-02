namespace DTOs.RoomDTOs
{
    public class GetByIdRoomDto
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Price { get; set; }
        public int BedCount { get; set; }
        public int BathCount { get; set; }
        public bool IsWifi { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool Status { get; set; } = false;
    }
}