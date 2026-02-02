using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.DAL.Concrete
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options){}

        public DbSet<About>? Abouts {get;set;}
        public DbSet<AboutImage>? AboutImages {get;set;}
        public DbSet<Contact>? Contacts {get;set;}
        public DbSet<Room>?  Rooms {get;set;}
        public DbSet<Slider>?  Sliders {get;set;}
        public DbSet<Staff>?  Staffs {get;set;}
        public DbSet<Testimonial>?  Testimonials {get;set;}
        public DbSet<Charge>? Charges {get;set;}
    }
}