using GraphiQl;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.MicrosoftDI;
using GraphQL.SystemTextJson;
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

builder.Services.AddGraphQL(builder =>
{
    builder.AddSystemTextJson()
        .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
        .AddGraphTypes()
        .AddDataLoader();
});

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
