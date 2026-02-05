namespace DTOs.BookingDTOs
{
    public class GetByIdBookingDto
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
    }
}