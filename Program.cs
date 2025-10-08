using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using dotnet_sp_api.Models.DBContextModels;
using Microsoft.EntityFrameworkCore;
using dotnet_sp_api.Configurations;
using dotnet_sp_api.Interfaces;
using dotnet_sp_api.Repositories.Interfaces;
using dotnet_sp_api.Helpers;
using dotnet_sp_api.Services.Implementations;
using dotnet_sp_api.Services.Interfaces;
using dotnet_sp_api.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// register or bind encryption setting here  
builder.Services.Configure<EncryptionSettings>(
    builder.Configuration.GetSection("EncryptionSettings")
);

//Set cors (Cross Origin Resource Sharing) here - a mechanism (or an HTTP protocol) to allow web apps to access resources hosted on different domains or origins. 
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin() // Angular app URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

// Register PostgreSQL DbContext
builder.Services.AddDbContext<DBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(); // optional
        }));

var sharedKey = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(sharedKey),
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer(); //mainly used to setup swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sports Profile API",
        Description = "RESTful API web service for the Sports Profile (SP) social networking application.<br/><br/>" +
            "Author: <b>Marc Manuel</b> (https://www.linkedin.com/in/marc-manuel-b298326/)<br/><br/>" +
            "Note: This version uses C#, .NET 8.0 (ASP.NET Core), Entity Framework, and a PostgreSQL database.<br/>" +
            "<div>" +
            "<h3>Quick Start with Swagger</h3>" +
            "<ol>" +
            "    <li>Go to <code>/api/account/Login</code> under the <strong>Account</strong> service.</li>" +
            "    <li>Login with:" +
            "    <ul>" +
            "        <li><strong>Email:</strong> <code>michael.jordan@outlook.com</code></li>" +
            "        <li><strong>Password:</strong> <code>123456</code></li>" +
            "    </ul>" +
            "    </li>" +
            "    <li>Copy the JWT token from the response.</li>" +
            "    <li>Click <strong>'Authorize'</strong> and enter:" +
            "    <pre><code>bearer&nbsp;&lt;your_JWT_token&gt;</code></pre>" +
            "    </li>" +
            "    <li>You're now ready to access secured endpoints.</li>" +
            "</ol>" +
            "</div>" +
            "<p>You can also create an account using the <a href='https://angular-sport-profiles.vercel.app/login' target='_blank'>Angular web application</a> that consumes this API service.</p>",

        Version = "1.0.0"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}
                     }
                 });

    //include xml comments coming from controller methods to swagger 
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(System.AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    //enable swagger annotation support to define schema for the set of elements of the API Spec., i.e., parameters, schema classes aka models, properties, etc.
    c.EnableAnnotations();
});

//configure logging for the SP API
builder.Services.AddLogging(lb =>
{
    //var loggingSection = builder.Configuration.GetSection("Logging");
    //lb.AddFilter(loggingSection);
});

//register services for Dependency Injection (DI)
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<Crypto>();

builder.Services.AddScoped<ICommonRepository, CommonRepository>();
builder.Services.AddScoped<ICommonService, CommonService>();

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddScoped<ISettingRepository, SettingRepository>();
builder.Services.AddScoped<ISettingService, SettingService>();

builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sports Profile API V1");
});

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
