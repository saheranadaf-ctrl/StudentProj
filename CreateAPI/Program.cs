using CreateAPI.DataAccess;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding dependency for DBContext
builder.Services.AddDbContext<StudentDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

CreateWebHostBuilder(args).Build().Run();


    static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.AddConsole(); // Log to the console
                logging.AddDebug();   // Log to debug output window in Visual Studio Code
            })
            .UseStartup<Program>() // Specify your startup class here if you have one
            .UseUrls("http://localhost:5000") // Set the URL of your API
            ;

