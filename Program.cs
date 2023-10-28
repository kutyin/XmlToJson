using Microsoft.AspNetCore.Server.Kestrel.Core;

using XmlToJson.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services
    .Configure<SaveCsvOptions>(
        builder.Configuration.GetSection(nameof(SaveCsvOptions)));

builder.Services.AddControllers();
builder.Services.Configure<KestrelServerOptions>(options => {
    options.AllowSynchronousIO = true;
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.MapControllers();

app.Run();