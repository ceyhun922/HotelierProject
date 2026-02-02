namespace EntityLayer.Concrete
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }=false;
        public List<Room>? Rooms {get;set;}
    }
}