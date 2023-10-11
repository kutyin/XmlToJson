using Microsoft.AspNetCore.Server.Kestrel.Core;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<KestrelServerOptions>(options => {
    options.AllowSynchronousIO = true;
});

WebApplication app = builder.Build();

//if (app.Environment.IsDevelopment()) {
//    app.UseDeveloperExceptionPage();
//} else {
//    app.UseHttpsRedirection();
//}

app.UseRouting();
app.MapControllers();

app.Run();