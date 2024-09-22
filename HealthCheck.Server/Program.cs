global using HealthCheck.Server;




// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
   .AddCheck("ICMP_01",new ICMPHealthCheck("www.ryadel.com", 100))
   .AddCheck("ICMP_02",new ICMPHealthCheck("www.google.com", 100))
.AddCheck("ICMP_03",new ICMPHealthCheck($"www.{Guid.NewGuid():N}.com", 100));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();















// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseHealthChecks(new PathString("/api/health"), new CustomHealthCheckOptions());
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();
