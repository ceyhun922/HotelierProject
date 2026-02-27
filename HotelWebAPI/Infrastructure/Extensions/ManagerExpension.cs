using Service.Abstract;
using Service.Concrete;

namespace HotelWebAPI.Infrastructure.Extensions
{
    public static class ManagerExpension
    {
        public static void AddManagerServices(this IServiceCollection services)
        {

            services.AddScoped<IAboutService, AboutManager>();
            services.AddScoped<IAboutImageService, AboutImageManager>();
            services.AddScoped<IChargeService, ChargeManager>();
            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<IRoomService, RoomManager>();
            services.AddScoped<ISliderService, SliderManager>();
            services.AddScoped<IStafService, StaffManager>();
            services.AddScoped<ITestimonialService, TestimonialManager>();
            services.AddScoped<IRoomTypeService, RoomTypeManager>();
            services.AddScoped<ITeamService, TeamManager>();
            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<IBookingService, BookingManager>();
            services.AddScoped<ILocationService, LocationManager>();
        }
    }
}