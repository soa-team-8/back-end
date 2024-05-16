using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.Administration;
using Explorer.API.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureSwagger(builder.Configuration);
const string corsPolicy = "_corsPolicy";
builder.Services.ConfigureCors(corsPolicy);
builder.Services.ConfigureAuth();

builder.Services.RegisterModules();
builder.Services.AddGrpc().AddJsonTranscoding();

var app = builder.Build();

/*if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}*/
app.UseDeveloperExceptionPage();
//app.UseSwagger();
//app.UseSwaggerUI();

app.UseRouting();
app.UseCors(corsPolicy);
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();
app.MapGrpcService<TourProtoController>();
app.MapGrpcService<CheckpointProtoControler>();
app.MapGrpcService<EquipmentProtoController>();

app.Run();

// Required for automated tests
namespace Explorer.API
{
    public partial class Program { }
}