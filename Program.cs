using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManage.Data;
using GraphQLPractive.Repositories;
using GraphQLPractive.Services;
using GraphQL;
using GraphQLPractive.Graphql;
using GraphQL.Types;
using GraphQLPractive.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));


builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddScoped<StudentQuery>();
builder.Services.AddScoped<StudentMutation>(); // ✅ Đăng ký Mutation
builder.Services.AddScoped<ISchema, StudentSchema>();

builder.Services.AddSingleton<KafkaProducerService>();
builder.Services.AddSingleton<KafkaConsumerService>();

//config graphql
builder.Services.AddGraphQL(graphQLBuilder =>
{
    graphQLBuilder
        .AddSystemTextJson()
        .AddGraphTypes() // 
        .AddErrorInfoProvider(opt => opt.ExposeExceptionDetails = true)
        .AddDataLoader();
    //gom các câu truy vấn =>
    //1: select * from student + 100 select * from class where id) (101 cau lenh)
    //dataloader =>
    //1: select * from student + 1 select * from class where id in (1,2,3) (2 cau lenh)
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
    builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseGraphQL<ISchema>();
app.UseGraphQLPlayground("/graphql");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
