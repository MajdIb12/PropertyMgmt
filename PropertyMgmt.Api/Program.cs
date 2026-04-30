using PropertyMgmt.Api;
using PropertyMgmt.Application;
using PropertyMgmt.Infrastructure;
using Microsoft.OpenApi.Models;
using PropertyMgmt.Api.Middleware.Exceptions;
using PropertyMgmt.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 1. تسجيل الخدمات
builder.Services.AddControllers(); // ضروري لتفعيل الـ Controllers
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "PropertyMgmt API", Version = "v1" });
    
    // اترك هذا فقط إذا كنت لا تزال تستخدم الـ Header كخيار احتياطي
    options.AddSecurityDefinition("tenantId", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "X-Tenant-Id",
        Type = SecuritySchemeType.ApiKey,
        Description = "Tenant ID (Optional if using Subdomains)"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "tenantId" }
            },
            Array.Empty<string>()
        }
    });
});

// تسجيل طبقات المشروع
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// التعامل مع الأخطاء بشكل مركزي
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// 2. إعداد الـ Middleware Pipeline (الترتيب حيوي جداً)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(); // يجب أن يكون في البداية
app.UseHttpsRedirection();
app.UseStaticFiles();

// الـ CORS يجب أن يكون قبل الـ Routing والـ Auth
app.UseCors("DefaultPolicy");

// الميدل وير الخاص بنا لتحديد المستأجر (قبل الـ Auth لكي يعرف الـ Auth سياق الطلب)

app.UseAuthentication();
app.UseMiddleware<TenantIdentificationMiddleware>();

app.UseAuthorization();

// تفعيل الخرائط للـ Controllers
app.MapControllers();

app.Run();