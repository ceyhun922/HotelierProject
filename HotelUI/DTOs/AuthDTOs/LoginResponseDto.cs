using Newtonsoft.Json;

namespace HotelUI.DTOs.AuthDTOs
{
    public class LoginResponseDto
    {
               public int UserId { get; set;}

        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string? RefreshToken { get; set; }
    }
}