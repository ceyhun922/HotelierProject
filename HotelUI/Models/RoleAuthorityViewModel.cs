using DTOs.RoleDTOs;
using DTOs.UserDTOs;

namespace HotelUI.Models
{
    public class RoleAuthorityViewModel
    {
         public GetByIdUserDto User { get; set; }
        public List<ResultRoleDto> Roles { get; set; }
        public int UserId { get; set; }
        public List<string> UserRoles { get; set; }
    }
}