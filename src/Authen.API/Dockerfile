#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Authen.API/Authen.API.csproj", "src/Authen.API/"]
COPY ["src/JWTAuthentication/JWTAuthentication.csproj", "src/JWTAuthentication/"]
RUN dotnet restore "src/Authen.API/Authen.API.csproj"
COPY . .
WORKDIR "/src/src/Authen.API"
RUN dotnet build "Authen.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authen.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authen.API.dll"]