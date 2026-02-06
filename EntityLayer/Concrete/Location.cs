namespace EntityLayer.Concrete
{
    public class Location
    {
        public int Id { get; set; }
        public string? Name {get;set;}

        public List<Room>? Rooms {get;set;}
    }
}