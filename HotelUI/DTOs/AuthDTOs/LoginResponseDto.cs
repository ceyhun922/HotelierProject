namespace HotelUI.DTOs.AuthDTOs
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string? RefreshToken { get; set; }
    }
}