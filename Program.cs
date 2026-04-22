using APBD_CW5_S27747.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

AppData.Initialize();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();