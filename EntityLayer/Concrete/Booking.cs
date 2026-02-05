using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class Booking
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Adult { get; set; }
        public string? Country { get; set; }
        public int RoomTypeId { get; set; }
        public string? Message { get; set; }
        public RoomType? RoomType { get; set; }
    }
}