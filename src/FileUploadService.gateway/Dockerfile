#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/FileUploadService.gateway/FileUploadService.gateway.csproj", "src/FileUploadService.gateway/"]
COPY ["src/JWTAuthentication/JWTAuthentication.csproj", "src/JWTAuthentication/"]
RUN dotnet restore "src/FileUploadService.gateway/FileUploadService.gateway.csproj"
COPY . .
WORKDIR "/src/src/FileUploadService.gateway"
RUN dotnet build "FileUploadService.gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileUploadService.gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileUploadService.gateway.dll"]