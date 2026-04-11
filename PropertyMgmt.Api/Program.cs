using PropertyMgmt.Api;
using PropertyMgmt.Application;
using PropertyMgmt.Infrastructure;
using Microsoft.OpenApi.Models; // تأكد من إضافة هذا السطر

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();

builder.Services.AddApiServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
// app.UseMiddleware<ExceptionHandlingMiddleware>();

builder.Services.AddEndpointsApiExplorer();

// التعديل هنا: إعداد Swagger ليدعم إدخال الـ TenantId
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "PropertyMgmt API", Version = "v1" });

    options.AddSecurityDefinition("tenantId", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "X-Tenant-Id", // اسم الـ Header الذي يبحث عنه نظامك
        Type = SecuritySchemeType.ApiKey,
        Description = "الرجاء إدخال Tenant ID (مثال: Tenant-A)"
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("DefaultPolicy");
app.UseAuthorization();
// app.MapControllers();

app.Run();