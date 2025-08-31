# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/API/WorkflowTracking.Users.API/WorkflowTracking.Users.API.csproj", "src/API/WorkflowTracking.Users.API/"]
COPY ["src/Common/WorkflowTracking.Common.Infrastructure/WorkflowTracking.Common.Infrastructure.csproj", "src/Common/WorkflowTracking.Common.Infrastructure/"]
COPY ["src/Common/WorkflowTracking.Common.Application/WorkflowTracking.Common.Application.csproj", "src/Common/WorkflowTracking.Common.Application/"]
COPY ["src/Common/WorkflowTracking.Common.Domain/WorkflowTracking.Common.Domain.csproj", "src/Common/WorkflowTracking.Common.Domain/"]
COPY ["src/Common/WorkflowTracking.Common.Presentation/WorkflowTracking.Common.Presentation.csproj", "src/Common/WorkflowTracking.Common.Presentation/"]
COPY ["src/Modules/Users/WorkflowTracking.Modules.Users.Infrastructure/WorkflowTracking.Modules.Users.Infrastructure.csproj", "src/Modules/Users/WorkflowTracking.Modules.Users.Infrastructure/"]
COPY ["src/Modules/Users/WorkflowTracking.Modules.Users.Application/WorkflowTracking.Modules.Users.Application.csproj", "src/Modules/Users/WorkflowTracking.Modules.Users.Application/"]
COPY ["src/Modules/Users/WorkflowTracking.Modules.Users.Domain/WorkflowTracking.Modules.Users.Domain.csproj", "src/Modules/Users/WorkflowTracking.Modules.Users.Domain/"]
COPY ["src/Modules/Users/WorkflowTracking.Modules.Users.IntegrationEvents/WorkflowTracking.Modules.Users.IntegrationEvents.csproj", "src/Modules/Users/WorkflowTracking.Modules.Users.IntegrationEvents/"]
COPY ["src/Modules/Users/WorkflowTracking.Modules.Users.Presentation/WorkflowTracking.Modules.Users.Presentation.csproj", "src/Modules/Users/WorkflowTracking.Modules.Users.Presentation/"]
RUN dotnet restore "./src/API/WorkflowTracking.Users.API/WorkflowTracking.Users.API.csproj"
COPY . .
WORKDIR "/src/src/API/WorkflowTracking.Users.API"
RUN dotnet build "./WorkflowTracking.Users.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WorkflowTracking.Users.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkflowTracking.Users.API.dll"]