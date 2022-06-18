using GraphQL;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using GraphQL_API.Models;
using GraphQL_API.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL_API.Controllers;

[Route("graphql")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ILogger<CourseController> _logger;
    private readonly ISchema _schema;

    public CourseController(ILogger<CourseController> logger, ISchema schema)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _schema = schema ?? throw new ArgumentNullException(nameof(schema));
    }

    [HttpGet]
    [Route("getcourses")]
    public async Task<string> GetCourses([FromQuery] string query)
    {
        var schema = Schema.For(@"
                    type Query {
                                   courses : [Course!]  
                                   course (id : ID!) : Course
                                 }
                    enum PaymentType {
                                        FREE ,
                                        PAID
                                  }
                    type Course {
                                    title: String!
                                    duration: Int
                                    level : String!
                                    instructor: String
                                    paymentType : PaymentType
                                    ratings : [Rating]
                                }
                    type Rating
                                {   
                                    studentName : String
                                    stars : Int
                                    review : String
                                }", builder =>
        {
            builder.Types.Include<Query>();
        });

        var json = await schema.ExecuteAsync(options =>
        {
            options.Query = query;
        });

        return json;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
    {
        var result = await new DocumentExecuter()
            .ExecuteAsync(executionOptions =>
            {
                executionOptions.Schema = _schema;
                executionOptions.Query = query.Query;
            });

        if (result.Errors?.Count > 0)
            return BadRequest(result.Errors);

        return Ok(result);
    }
}
