using Microsoft.AspNetCore.DataProtection;
using NotesService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContextPool<NotesDbContext>(opt => 
  opt.UseNpgsql(builder.Configuration.GetConnectionString("NotesDatabase")));

builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, "keys"))).SetApplicationName("Notes");

builder.Services.AddScoped<DataProtection>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
