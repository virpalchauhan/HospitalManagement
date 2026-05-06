using HospitalManagement.Entity;
using HospitalManagement.Filters;
using HospitalManagement.Helper;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔐 JWT CONFIG
var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    // 🔥 IMPORTANT: Cookie se JWT read karega
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Cookies["AuthToken"];

            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.ConfigureFilter(new AuthPageFilter());
});

// 🗄️ DATABASE
builder.Services.AddDbContext<EntityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));

// 🧩 SERVICES
builder.Services.AddScoped<IDepartmentTblServices, DepartmentTblServices>();
builder.Services.AddScoped<IDoctorNurseApplicationServices, DoctorNurseApplicationServices>();
builder.Services.AddScoped<IDoctorAndNurseServices, DoctorAndNurseServices>();
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<IJwtTokenHelper, JwtTokenHelper>();
builder.Services.AddScoped<ISocialMediaMastersServices, SocialMediaMastersServices>();
builder.Services.AddScoped<AuthPageFilter>();

// 🧠 SESSION
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 📄 RAZOR PAGES
builder.Services.AddRazorPages();

var app = builder.Build();

// ⚙️ PIPELINE
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();          // optional
app.UseAuthentication();   // 🔐 MUST
app.UseAuthorization();    // 🔐 MUST

app.MapRazorPages();

app.Run();