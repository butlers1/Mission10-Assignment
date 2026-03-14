var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors(x => x.WithOrigins("http://localhost:3000"));

app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();

app.Run();