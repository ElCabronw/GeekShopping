
using GeekShopping.PaymentAPI.MessageConsumer;
using GeekShopping.PaymentAPI.RabbitMQSender;
using GeekShopping.PaymentProcessor;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = builder.Configuration.GetConnectionString("MySQLConnectionString");

//builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 28))));
//
//var contextBuilder = new DbContextOptionsBuilder<MySQLContext>();
//contextBuilder.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 28)));



builder.Services.AddHostedService<RabbitMQPaymentConsumer>();
//RabbitMQPaymentConsumer
builder.Services.AddSingleton<IProcessPayment, ProcessPayment>(); //RabbitMQMessageSender
builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

//builder.Services.AddScoped<ICartRepository, CartRepository>();
//builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7287/";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "geek_shopping");

    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping.PaymentAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer' [space] and your token!",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
       new OpenApiSecurityScheme
       {
           Reference = new OpenApiReference
           {
               Type = ReferenceType.SecurityScheme,
               Id = "Bearer"
           },
           Scheme = "oauth2",
           Name = "Bearer",
           In = ParameterLocation.Header
       },
       new List<string>()

    }});
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();

