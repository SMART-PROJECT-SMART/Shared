
using Shared.Services;
using Shared.Services.ICDDirectory;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddAppConfiguration(builder.Configuration);
builder.Services.AddIcdDirectoryServices();

var app = builder.Build();
app.Services.GetRequiredService<IICDDirectory>().LoadAllICDs();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.Run();
