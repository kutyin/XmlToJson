using Microsoft.AspNetCore.Server.Kestrel.Core;
using XmlToJson.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<KestrelServerOptions>(options => {
    options.AllowSynchronousIO = true;
});

builder.Services.AddScoped<TrackingCsvBuilder>()
    .AddScoped<XmlToJsonConverter>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.MapControllers();

app.Run();