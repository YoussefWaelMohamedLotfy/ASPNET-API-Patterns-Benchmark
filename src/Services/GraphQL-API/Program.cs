using GraphiQl;
using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using GraphQL_API.Data;
using GraphQL_API.Queries;
using GraphQL_API.Schemas;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CourseDbContext>(options =>
    options.UseSqlite("Data Source=coursedb.sqlite"));

builder.Services.AddScoped<ISchema, CourseSchema>();
builder.Services.AddScoped<ProQuery>();

builder.Services.AddGraphQL(options => { options.EnableMetrics = false; })
    .AddSystemTextJson()
    .AddGraphTypes(ServiceLifetime.Scoped);

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseGraphiQl("/graphql");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseGraphQL<ISchema>();

app.UseMvc();

app.Run();
