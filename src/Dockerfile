FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Website.Api/Website.Api.csproj", "Website/Website.Api/"]
COPY ["Website.Application/Website.Application.csproj", "Website/Website.Application/"]
COPY ["Website.Domain/Website.Domain.csproj", "Website/Website.Domain/"]
COPY ["Website.Common/Website.Common.csproj", "Website/Website.Common/"]
COPY ["Website.Infrastructure/Website.Infrastructure.csproj", "Website/Website.Infrastructure/"]
COPY ["Website.Services/Website.Services.csproj", "Website/Website.Services/"]
RUN dotnet restore "Website/Website.Api/Website.Api.csproj"
WORKDIR /src/Website
COPY ./Website.Api ./Website.Api
COPY ./Website.Application ./Website.Application
COPY ./Website.Domain ./Website.Domain
COPY ./Website.Common ./Website.Common
COPY ./Website.Infrastructure ./Website.Infrastructure
COPY ./Website.Services ./Website.Services
COPY ["server-cert.crt", "/usr/local/share/ca-certificates/"]
COPY . .
RUN chmod 644 /usr/local/share/ca-certificates/server-cert.crt && update-ca-certificates
RUN dotnet build "Website.Api/Website.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Website.Api/Website.Api.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt-get update && apt-get install -y iputils-ping && apt-get install -y curl
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Website.Api.dll"]
