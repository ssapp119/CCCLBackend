
var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<QandA.Data.IDataRepository, QandA.Data.DataRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                          builder.WithOrigins("https://carrollcountyservices.com","http://carrollcountyservices.com", "https://www.carrollcountyservices.com", "http://www.carrollcountyservices.com", "https://carrollcountychristmaslights.com", "http://carrollcountychristmaslights.com", "https://www.carrollcountychristmaslights.com", "http://www.carrollcountychristmaslights.com", "http://localhost:3001").AllowAnyHeader().AllowAnyMethod());             
                      });
   /* options.AddPolicy(name: MyAllowSpecificOrigins,
         builder => builder.WithOrigins("https://carrollcountychristmaslights.com", "http://carrollcountychristmaslights.com", "https://www.carrollcountychristmaslights.com", "http://www.carrollcountychristmaslights.com").AllowAnyHeader().AllowAnyMethod());
});
                   /* builder =>
                        builder.WithOrigins("https://carrollcountychristmaslights.com", "http://carrollcountychristmaslights.com").AllowAnyHeader().AllowAnyMethod());             
                      });*/

//.setHeader("Access-Control-Allow-Origin", "*");
//response.setHeader("Access-Control-Allow-Credentials", "true");
//response.setHeader("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
//response.setHeader("Access-Control-Allow-Headers", "Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers");


//builder.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3001")));//Configuration["Frontend"])));
var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{ ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto }
    );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
