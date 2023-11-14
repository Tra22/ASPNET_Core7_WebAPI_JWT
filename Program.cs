using System.Diagnostics;
using System.Text;
using Asp.Versioning;
using ASPNET_Core7_WebAPI_JWT.Configuration;
using ASPNET_Core7_WebAPI_JWT.Entities;
using ASPNET_Core7_WebAPI_JWT.Middleware.ExceptionHandler;
using ASPNET_Core7_WebAPI_JWT.Model.Identity;
using ASPNET_Core7_WebAPI_JWT.Payload.Global;
using ASPNET_Core7_WebAPI_JWT.Policies.Handler;
using ASPNET_Core7_WebAPI_JWT.Policies.Requirement;
using ASPNET_Core7_WebAPI_JWT.Services;
using ASPNET_Core7_WebAPI_JWT.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

const string SwaggerRoutePrefix = "api-docs";

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddDbContext<DataContext>
    (options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AuthenticationUser, AuthenticationRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"] ?? "default_secret"))
    };
});

//add handler for policies
builder.Services.AddSingleton<IAuthorizationHandler, HasPhoneNumberAndConfirmedHandler>();

builder.Services.AddAuthorization(options =>
{
    //ClaimBaseController
    options.AddPolicy("UserOnly", policy => policy.RequireClaim("UserNumberID"));
    options.AddPolicy("UserSpecificID", policy => policy.RequireClaim("UserNumberID", "111", "222"));

    //PolicyBaseController
    options.AddPolicy("HasClaimAsAdmin", policy =>
         policy.RequireAssertion(context =>
             context.User.HasClaim(c =>
                     (c.Type == "Admin" || c.Type == "SuperAdmin")
                     && c.Value == "true"
                     && c.Issuer == "http://localhost:5000"
                 )
             )
         );
    //PolicyCustomController
    options.AddPolicy("HasPhoneNumberAndConfirmed", policy =>
        policy.Requirements.Add(new HasPhoneNumberConfirmedRequirement(true)));
});

builder.Services
    .AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                })
    .AddApiExplorer(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        // Add the versioned API explorer, which also adds IApiVersionDescriptionProvider service
        // note: the specified format code will format the version as "'v'major[.minor][-status]"
        options.GroupNameFormat = "'v'VVV";

        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
        // can also be used to control the format of the API version in route templates
        options.SubstituteApiVersionInUrl = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
    });
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
     {
         options.InvalidModelStateResponseFactory = context =>
         {
             var result = new Response<ValidationResultModel> { 
                Data = new ValidationResultModel(context.ModelState),
                Success = false,
                Error = "Validation error.",
                TraceId = context.HttpContext.TraceIdentifier ?? Activity.Current?.Id
            };
            return new BadRequestObjectResult(result)
            {
                ContentTypes = { "application/problem+json" }
            };
         };
     });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    // Add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();
});

//inject Data Access Layer - Repository


//inject Service layer
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddControllers();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { options.RouteTemplate = $"{SwaggerRoutePrefix}/{{documentName}}/docs.json"; });
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = SwaggerRoutePrefix;
        foreach (var description in app.DescribeApiVersions())
            options.SwaggerEndpoint($"/{SwaggerRoutePrefix}/{description.GroupName}/docs.json", description.GroupName.ToUpperInvariant());
    });
}
app.UseSerilogRequestLogging();

//Middleware 
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

//Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

