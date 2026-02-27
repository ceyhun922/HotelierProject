

using HotelWebAPI.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseService(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddCorsService();
builder.Services.AddJWTService(builder.Configuration);



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddRepositoriesService();
builder.Services.AddManagerServices();


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
