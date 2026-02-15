namespace EntityLayer.Concrete
{
    public class Location
    {
        public int Id { get; set; }
        public string? Name {get;set;}
        public bool Status {get;set;}=false;

        public List<Room>? Rooms {get;set;}
    }
}