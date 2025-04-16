using Microsoft.EntityFrameworkCore;
using WebApplication3API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<QuestionContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CRUDCS")));


builder.Services.AddDbContext<LoginContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CRUDCS")));

builder.Services.AddDbContext<RegisterContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CRUDCS")));
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Needed for Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// ✅ Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// ✅ Enable Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Swagger at root URL
});

// ✅ Enable CORS
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ✅ Configure default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();