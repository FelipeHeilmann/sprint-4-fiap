using WiseBuddy.Api.Services;
using WiseBuddy.Api.Repositories;
using WiseBuddy.Api.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using WiseBuddy.Api.Data.Context;
using WiseBuddy.Api.Gateway;
using Microsoft.Extensions.Caching.Memory;

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

builder.Services.AddHttpClient<IMarketGateway, CoinGeckoGateway>()
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler());
builder.Services.AddScoped<IMarketGateway>(sp =>
{
    var client = sp.GetRequiredService<HttpClient>();
    var cache = sp.GetRequiredService<IMemoryCache>();
    return new CoinGeckoGateway(client, cache);
});

var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
