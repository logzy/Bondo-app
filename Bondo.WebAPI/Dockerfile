#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Bondo.WebAPI/Bondo.WebAPI.csproj", "Bondo.WebAPI/"]
COPY ["Bondo.Persistence/Bondo.Persistence.csproj", "Bondo.Persistence/"]
COPY ["Bondo.Domain/Bondo.Domain.csproj", "Bondo.Domain/"]
COPY ["Bondo.Application/Bondo.Application.csproj", "Bondo.Application/"]
COPY ["Bondo.Shared/Bondo.Shared.csproj", "Bondo.Shared/"]
COPY ["Bondo.Infrastructure/Bondo.Infrastructure.csproj", "Bondo.Infrastructure/"]
RUN dotnet restore "Bondo.WebAPI/Bondo.WebAPI.csproj"
COPY . .
WORKDIR "/src/Bondo.WebAPI"
RUN dotnet build "Bondo.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bondo.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bondo.WebAPI.dll"]