using Microsoft.EntityFrameworkCore;
using UserManagementApp.API;
using UserManagementApp.Application.Middlewares;
using UserManagementApp.Infrastructure.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.RegisterExtension();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//chạy migration khi khởi động ứng dụng
app.ApplyMigration();

// Apply pending migrations automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserManagementAppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseRouting();

app.UseCors("MyPolicy");

app.UseAuthentication();
app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
