# ========================================================
# المرحلة 1: بيئة التشغيل النهائية (Runtime) - الغرفة النظيفة
# ========================================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ========================================================
# المرحلة 2: بيئة التطوير والبناء (SDK) - المطبخ الثقيل
# ========================================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 🛠️ لقطة ذكية: نسخ ملف الـ csproj أولاً وعمل Restore لتسريع بناء الحاوية مستقبلاً (Caching)
COPY ["PropertyMgmt.Api/PropertyMgmt.Api.csproj", "PropertyMgmt.Api/"]
RUN dotnet restore "PropertyMgmt.Api/PropertyMgmt.Api.csproj"

# الآن ننسخ بقية كود السي شارب بالكامل إلى الحاوية
COPY . .
WORKDIR "/src/PropertyMgmt.Api"

# بناء المشروع وعمل Build في مجلد مؤقت اسمه app/build
RUN dotnet build "PropertyMgmt.Api.csproj" -c Release -o /app/build

# ========================================================
# المرحلة 3: التجهيز والنشر (Publish)
# ========================================================
FROM build AS publish
RUN dotnet publish "PropertyMgmt.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ========================================================
# المرحلة 4: إنتاج الحاوية النهائية (Final) - التقديم
# ========================================================
FROM base AS final
WORKDIR /app
# 🎯 السر هنا: سرقة المجلد الجاهز فقط من مرحلة الـ publish ورمي الـ SDK بالكامل
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PropertyMgmt.Api.dll"]