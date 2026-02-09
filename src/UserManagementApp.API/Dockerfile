# === STAGE 1: BUILD ===
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy các file dự án để restore trước (Tối ưu cache)
COPY UserManagementApp.slnx ./
COPY src/UserManagementApp.API/UserManagementApp.API.csproj src/UserManagementApp.API/
COPY src/UserManagementApp.Application/UserManagementApp.Application.csproj src/UserManagementApp.Application/
COPY src/UserManagementApp.Domain/UserManagementApp.Domain.csproj src/UserManagementApp.Domain/
COPY src/UserManagementApp.Infrastructure/UserManagementApp.Infrastructure.csproj src/UserManagementApp.Infrastructure/

RUN dotnet restore src/UserManagementApp.API/UserManagementApp.API.csproj

# Copy toàn bộ code còn lại
COPY src/UserManagementApp.API/ src/UserManagementApp.API/
COPY src/UserManagementApp.Application/ src/UserManagementApp.Application/
COPY src/UserManagementApp.Domain/ src/UserManagementApp.Domain/
COPY src/UserManagementApp.Infrastructure/ src/UserManagementApp.Infrastructure/

WORKDIR /app/src/UserManagementApp.API
RUN dotnet publish -c Release -o /app/out

# === STAGE 2: RUNTIME ===
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/out .

# --- KHẮC PHỤC LỖI TẠI ĐÂY ---
# 1. Ép cứng ứng dụng chạy cổng 80
ENV ASPNETCORE_HTTP_PORTS=80

# 2. Khai báo mở cổng 80 cho Azure biết
EXPOSE 80

ENTRYPOINT ["dotnet", "UserManagementApp.API.dll"]