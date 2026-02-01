using System.ComponentModel.DataAnnotations;

namespace Hotelier.EntityLayer.Concrete
{
    public class AboutImage
    {
        [Key]
        public int Id { get; set; }
        public string? AboutImageUrl { get; set; }

        public int AboutId {get;set;}
        public About About {get;set;}
    }
}