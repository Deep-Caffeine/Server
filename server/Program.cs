using Microsoft.EntityFrameworkCore;
using server;
using server.Middlewares;
using server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_developCorsDisable",
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
});

// Add services to the container.
builder.Services.AddControllers();

// Dependency injection (services)
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ChatHttpService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    //개발 상일 경우 로컬의 SQLite로 연결
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        // Local SQLite DbPath :: ~/.local/share/data.db
        string DbPath = System.IO.Path.Join(path, "./data.db");
        options.UseSqlite($"Data Source={DbPath}");
    });
}
else
{
    //배포 상태인 경우 MySQL로 연결
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        string connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};";
        connectionString += $"Database={Environment.GetEnvironmentVariable("DB_NAME")};";
        connectionString += $"Uid={Environment.GetEnvironmentVariable("DB_USER")};";
        connectionString += $"Pwd={Environment.GetEnvironmentVariable("DB_PASSWORD")};";

        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
bool isSwagger = app.Configuration.GetValue<bool>("swagger");

if (app.Environment.IsDevelopment() || isSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("_developCorsDisable");

//app.UseMiddleware<CorsMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
