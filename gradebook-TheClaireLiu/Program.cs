using GradeBook.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<GradeBookContext>(
    options => options.UseSqlite("Data Source=GradeBook.sqlite")
);

var app = builder.Build();

app.MapControllers();

app.Run();
