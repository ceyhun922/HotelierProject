using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.DAL.Concrete
{
    public class ApiContext : IdentityDbContext<User,Role,int>
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options){}

        public DbSet<About>? Abouts {get;set;}
        public DbSet<AboutImage>? AboutImages {get;set;}
        public DbSet<Contact>? Contacts {get;set;}
        public DbSet<Room>?  Rooms {get;set;}
        public DbSet<RoomType>? RoomTypes {get;set;}
        public DbSet<Slider>?  Sliders {get;set;}
        public DbSet<Staff>?  Staffs {get;set;}
        public DbSet<Testimonial>?  Testimonials {get;set;}
        public DbSet<Team>?  Teams {get;set;}
        public DbSet<Charge>? Charges {get;set;}
        public DbSet<Message>? Messages {get;set;}
        public DbSet<Booking>? Bookings {get;set;}
        public DbSet<Location>? Locations {get;set;}
    }
}