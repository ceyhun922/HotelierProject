using HotelWebAPI.Infrastructure.Extensions;
using HotelWebAPI.Infrastructure.Seed;
using Service.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseService(builder.Configuration);
builder.Services.AddJWTService(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddCorsService();
builder.Services.AddRepositoriesService();
builder.Services.AddManagerServices();

builder.Services.AddAutoMapper(typeof(GeneralMapping)); 


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("HotelApi");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var config = services.GetRequiredService<IConfiguration>();
    await IdentitySeedData.CreateAdminUser(services, config);
}
app.Run();
