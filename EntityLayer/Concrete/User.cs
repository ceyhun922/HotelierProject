using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
    public class User : IdentityUser<int>
    {
        public int ImageUrl { get; set; }
    }
}