using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moteqin.Domain.Entity;
using Moteqin.Domain.Interfaces;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;
using Moteqin.Infrastructure.Implementation.Repository;
using Moteqin.Infrastructure.Persistence.Repositories;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

#region DbContext
builder.Services.AddDbContext<MoteqinDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Moteqin")
    )
);
#endregion

#region Identity (IMPORTANT FOR AUTH)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // 🔥 مهم جدًا لمنع مشاكل التكرار والـ weak passwords
    options.User.RequireUniqueEmail = true;

    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<MoteqinDbContext>()
.AddDefaultTokenProviders();
#endregion

#region JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        )
    };
});
#endregion

#region Authorization (IMPORTANT)
builder.Services.AddAuthorization();
#endregion

#region MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(Moteqin.Application.ApplicationAssemblyMarker).Assembly
    );
});
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(
    typeof(Moteqin.Application.ApplicationAssemblyMarker).Assembly
);
#endregion

#region Auth Services (IMPORTANT)
builder.Services.AddScoped<IJwtService, JwtService>();
#endregion

#region Repositories
builder.Services.AddScoped<IRecordingRepository, RecordingRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IUserProgressRepository, UserProgressRepository>();
builder.Services.AddScoped<ISurahRepository, SurahRepository>();
builder.Services.AddScoped<IAyahRepository, AyahRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IGroupMemberRepository, GroupMemberRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IStreakRepository, StreakRepository>();
builder.Services.AddScoped<IPointRepository, PointRepository>();
builder.Services.AddScoped<IDailyPlanRepository, DailyPlanRepository>();
#endregion

#region Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region Controllers + Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

var app = builder.Build();

#region Pipeline (IMPORTANT ORDER FOR AUTH)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔥 مهم جدًا الترتيب
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
#endregion
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MoteqinDbContext>();
    await DbInitializer.SeedAsync(context);
}

app.Run();