using WiseBuddy.Api.Services;
using WiseBuddy.Api.Repositories;
using WiseBuddy.Api.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using WiseBuddy.Api.Data.Context;
using WiseBuddy.Api.Gateway;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<SuitabilityService>();
builder.Services.AddScoped<RecomendacaoService>();
builder.Services.AddScoped<CotacaoService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ISuitabilityRepository, SuitabilityRepository>();
builder.Services.AddScoped<IRecomendacaoRepository, RecomendacaoRepository>();
builder.Services.AddHttpClient<IMarketGateway, CoinGeckoGateway>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
