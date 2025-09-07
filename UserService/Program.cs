using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton(x => new JwtSigner(builder.Configuration["Jwt:Key"]!, builder.Configuration["Jwt:Issuer"]!, builder.Configuration["Jwt:Audience"]!));

builder.Services.AddDbContextPool<UserDbContext>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("UserDatabase")));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
