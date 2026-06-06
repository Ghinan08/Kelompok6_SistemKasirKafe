using KasirKafe_Kel6.Data;
using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Repositories;
using KasirKafe_Kel6.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<KalkulasiHargaService>();
builder.Services.Configure<TokoSettings>(
    builder.Configuration.GetSection("TokoSettings"));
builder.Services.AddScoped<PromoService>();
builder.Services.AddScoped<PencetakanService>();
builder.Services.AddScoped<BahanBakuService>();
builder.Services.AddScoped<PesananService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();