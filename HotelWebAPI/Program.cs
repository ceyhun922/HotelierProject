
using DAL.Abstract;
using DAL.Entityframework;
using DAL.GenericRepository;
using EntityLayer.Concrete;
using Hotelier.DAL.Abstract;
using Hotelier.DAL.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.Concrete;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"), t=>t.MigrationsAssembly("HotelWebAPI"));
});

builder.Services.AddIdentity<User,Role>(opt =>
{
    opt.Password.RequireDigit =false;
    opt.Password.RequiredLength =0;
    opt.Password.RequiredUniqueChars =0;
    opt.Password.RequireLowercase =false;
    opt.Password.RequireNonAlphanumeric =false;
    opt.Password.RequireUppercase =false;
})
.AddEntityFrameworkStores<ApiContext>()
.AddDefaultTokenProviders();


builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAboutDal, EFAboutRepository>();
builder.Services.AddScoped<IAboutImageDal, EFAboutImageRepository>();
builder.Services.AddScoped<IChargeDal, EFChargeRepository>();
builder.Services.AddScoped<IContactDal, EFContactRepository>();
builder.Services.AddScoped<IRoomDal, EFRoomRepository>();
builder.Services.AddScoped<ISliderDal, EFSliderRepository>();
builder.Services.AddScoped<IStaffDal, EFStaffRepository>();
builder.Services.AddScoped<ITestimonialDal, EFTestimonialRepository>();
builder.Services.AddScoped<IRoomTypeDal, EFRoomTypeRepository>();
builder.Services.AddScoped<ITeamDal, EFTeamRepository>();
builder.Services.AddScoped<IMessageDal, EFMessageRepository>();

builder.Services.AddScoped<IAboutService , AboutManager>();
builder.Services.AddScoped<IAboutImageService , AboutImageManager>();
builder.Services.AddScoped<IChargeService , ChargeManager>();
builder.Services.AddScoped<IContactService , ContactManager>();
builder.Services.AddScoped<IRoomService , RoomManager>();
builder.Services.AddScoped<ISliderService , SliderManager>();
builder.Services.AddScoped<IStafService , StaffManager>();
builder.Services.AddScoped<ITestimonialService , TestimonialManager>();
builder.Services.AddScoped<IRoomTypeService , RoomTypeManager>();
builder.Services.AddScoped<ITeamService , TeamManager>();
builder.Services.AddScoped<IMessageService , MessageManager>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("HotelApi",opts =>
    {
        opts.AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("HotelApi");
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();
