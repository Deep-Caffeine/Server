using server;
using server.Middlewares;
using server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Dependency injection (services)
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
