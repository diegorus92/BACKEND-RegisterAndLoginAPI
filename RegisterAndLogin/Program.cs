using Microsoft.EntityFrameworkCore;
using RegisterAndLoginAPI_BL.Services;
using RegisterAndLoginAPI_DAL.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>(
    options => {
        options.UseSqlServer(builder.Configuration.GetSection("ConnectionString")["AppConnection"]);
        }
    );
builder.Services.AddScoped<DbContext, Context>();

builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
