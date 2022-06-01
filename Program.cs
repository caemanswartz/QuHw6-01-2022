using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using QuHwJwtAspNetCoreWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
    {
        c.SwaggerDoc("v0", new OpenApiInfo {
           Title="UserAPIv0",
           Description="Register, Login, and Authenticate",
           Version="v0"});
    });
builder.Services.AddDbContext<ApiDbContext>(options=>
    {
        options.UseInMemoryDatabase("items");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c=>
    {
        c.SwaggerEndpoint("/swagger/v0/swagger.json","UserAPIv0");
    });

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGet("/users",
    async (ApiDbContext db)=>
        await db.Users.ToListAsync());
app.MapGet("/users{username}",
    async (ApiDbContext db, string username)=>
        await db.Users.FindAsync(username));
app.MapControllers();

app.Run();