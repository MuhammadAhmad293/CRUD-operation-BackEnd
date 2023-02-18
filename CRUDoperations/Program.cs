using CRUDoperations;
using CRUDoperations.Repositories.Resolver;
using CRUDoperations.Repositories.User;
using CRUDoperations.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

UnitOfWorkResolver.ResolveUintOfWork(builder.Services, builder.Configuration);
UnitOfWorkResolver.ResolveLazier(builder.Services, builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();//
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();//
builder.Services.AddSwaggerGen();//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())//
{
    app.UseSwagger();//
    //app.UseSwaggerUI();//
    app.UseSwaggerUI(c =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        // this line to run swagger when publish api to IIS 
        string SwaggerUrl = builder.Configuration.GetValue<string>("SwaggerUrl");
        // Replace {...} with microservice name
        c.SwaggerEndpoint($"{SwaggerUrl}/swagger/v1/swagger.json", "CRUD Opertions Api V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();//

app.UseMiddleware(typeof(ErrorHandlingMiddleware));

app.UseAuthorization();//

app.MapControllers();

app.Run();