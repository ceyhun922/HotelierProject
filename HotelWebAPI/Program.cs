

using System.Text;
using DAL.Abstract;
using DAL.Entityframework;
using DAL.GenericRepository;
using EntityLayer.Concrete;
using Hotelier.DAL.Abstract;
using Hotelier.DAL.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service.Abstract;
using Service.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<ApiContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"),
        t => t.MigrationsAssembly("HotelWebAPI"));
});

builder.Services.AddIdentity<User, Role>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 0;
    opt.Password.RequiredUniqueChars = 0;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApiContext>()
.AddDefaultTokenProviders();


var jwt = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwt["Key"]!);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("HotelApi", opts =>
    {
        opts.WithOrigins("https://localhost:7191")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwt["Issuer"],

        ValidateAudience = true,
        ValidAudience = jwt["Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromSeconds(30)
    };
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
builder.Services.AddScoped<IBookingDal, EFBookingRepository>();
builder.Services.AddScoped<ILocationDal, EFLocationRepository>();

builder.Services.AddScoped<IAboutService, AboutManager>();
builder.Services.AddScoped<IAboutImageService, AboutImageManager>();
builder.Services.AddScoped<IChargeService, ChargeManager>();
builder.Services.AddScoped<IContactService, ContactManager>();
builder.Services.AddScoped<IRoomService, RoomManager>();
builder.Services.AddScoped<ISliderService, SliderManager>();
builder.Services.AddScoped<IStafService, StaffManager>();
builder.Services.AddScoped<ITestimonialService, TestimonialManager>();
builder.Services.AddScoped<IRoomTypeService, RoomTypeManager>();
builder.Services.AddScoped<ITeamService, TeamManager>();
builder.Services.AddScoped<IMessageService, MessageManager>();
builder.Services.AddScoped<IBookingService, BookingManager>();
builder.Services.AddScoped<ILocationService, LocationManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("HotelApi");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
