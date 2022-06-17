using GraphiQl;
using GraphQL_API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CourseDbContext>(options =>
    options.UseSqlite("Data Source=coursedb.sqlite"));

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseGraphiQl("/graphql");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMvc();

app.Run();
