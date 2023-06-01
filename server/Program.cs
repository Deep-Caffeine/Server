using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using server;
using server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Dependency injection (services)
// builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<UserService>();

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
        string DbPath = System.IO.Path.Join(path, "./data.db");
        options.UseSqlite($"Data Source={DbPath}");
    });
}
else
{
    //배포 상태인 경우 MySQL로 연결
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        //추후 환경변수로 변경 필요
        string connectionString = "Server=localhost;Database=deepcaffeine;Uid=root;Pwd=toor;";
        options.UseSqlServer(connectionString);
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

app.Use((context, next) =>
{
    context.Response.Headers["Access-Control-Allow-Origin"] = "*";
    context.Response.Headers["Access-Control-Allow-Header"] = "*";
    context.Response.Headers["Access-Control-Allow-Method"] = "*";
    return next.Invoke();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
