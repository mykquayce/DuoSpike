using DuoSpike.BlazorApp1.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DuoConfig>(builder.Configuration);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<DuoUniversal.Client>(provider =>
{
    var config = provider.GetRequiredService<IOptions<DuoConfig>>().Value;
    var builder = new DuoUniversal.ClientBuilder(config.ClientId, config.ClientSecret, config.ApiHost, config.RedirectUri);
    return builder.Build();
});
builder.Services.AddSingleton<IMemoryCache>(new MemoryCache(new MemoryCacheOptions()));
builder.Services.AddScoped<SessionState>(_ => new SessionState());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
