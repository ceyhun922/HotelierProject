using DAL.Abstract;
using DAL.Entityframework;
using DAL.GenericRepository;
using Hotelier.DAL.Abstract;

namespace HotelWebAPI.Infrastructure.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddRepositoriesService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
            services.AddScoped<IAboutDal, EFAboutRepository>();
            services.AddScoped<IAboutImageDal, EFAboutImageRepository>();
            services.AddScoped<IChargeDal, EFChargeRepository>();
            services.AddScoped<IContactDal, EFContactRepository>();
            services.AddScoped<IRoomDal, EFRoomRepository>();
            services.AddScoped<ISliderDal, EFSliderRepository>();
            services.AddScoped<IStaffDal, EFStaffRepository>();
            services.AddScoped<ITestimonialDal, EFTestimonialRepository>();
            services.AddScoped<IRoomTypeDal, EFRoomTypeRepository>();
            services.AddScoped<ITeamDal, EFTeamRepository>();
            services.AddScoped<IMessageDal, EFMessageRepository>();
            services.AddScoped<IBookingDal, EFBookingRepository>();
            services.AddScoped<ILocationDal, EFLocationRepository>();
        }
    }
}