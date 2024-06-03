using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestWebApplicationHTTP.Data;
using Microsoft.IdentityModel.Tokens;
using TestWebApplicationHTTP;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting.Internal;
using System.Reflection;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Places", "Admin");
    options.Conventions.AuthorizeFolder("/Shared", "Admin");
    options.Conventions.AuthorizeFolder("/Users", "Admin");
    options.Conventions.AuthorizePage("/charts", "Admin");
    options.Conventions.AuthorizePage("/administration", "Admin");
});

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContext") ?? throw new InvalidOperationException("Connection string 'ApplicationContext' not found.")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // ����� �� ���������
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ��������� �������� ������
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            // ��������� ����������� ������
            ValidateAudience = false,
            // ��������� ������� ������������� ������
            ValidateLifetime = true,
            // ��������� ����� ������������
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // ��������� ����� ������������
            ValidateIssuerSigningKey = true,
        };
    })
    // ����� Admin
    .AddJwtBearer("Admin", options =>
    {
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ��������� �������� ������
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            // ��������� ����������� ������
            ValidateAudience = true,
            ValidAudience = "Admin",
            // ��������� ������� ������������� ������
            ValidateLifetime = true,
            // ��������� ����� ������������
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // ��������� ����� ������������
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddAuthorization(options =>
{
    // �������� ��� �������������
    options.AddPolicy("Default", policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Default");
    });
    // �������� ��� ��������������
    options.AddPolicy("Admin", policy =>
    {
        policy.AuthenticationSchemes.Add("Admin");
        policy.RequireAuthenticatedUser();
    });
});
//var assembly = Assembly.GetExecutingAssembly();
//var attribute = (System.Runtime.InteropServices.GuidAttribute)assembly.GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), true)[0];
//var GUID = attribute.Value;

builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

builder.WebHost.ConfigureKestrel(serveroption =>
{
    serveroption.ListenAnyIP(5163, listenoption =>
    {
        listenoption.UseHttps(httpsoptions =>
        {
            var exampleCert = CertificateLoader.LoadFromStoreCert(
    "parkingcontrolcomplex.ru", "My", StoreLocation.CurrentUser,
    allowInvalid: true);

            httpsoptions.ServerCertificate = exampleCert;
        });
    });
});

var app = builder.Build();

app.UseSession();

app.Use(async (context, next) =>
{
    // ���������� ���������� ������� ��� ���������� ���� �����������
    context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    // �������� ���������� �� ����� � Coockies
    var token = context.Request.Cookies.Where(x => x.Key == "jwtToken");
    if (token.FirstOrDefault().Key != null)
    {
        // ���� ����� ����������, �������� ��� � ���������� �������
        context.Request.Headers.Append("Authorization", "Bearer " + token.FirstOrDefault().Value);
    }
    await next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationContext>();
    //context.Database.EnsureCreated();
    //DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
