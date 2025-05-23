FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

USER $APP_UID
WORKDIR /app

# it documents that the container exposes these ports — but it doesn’t make the app listen on them.
EXPOSE 8030
EXPOSE 8031

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src

# Copy project files
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/DevHabit.Api/DevHabit.Api.csproj", "src/DevHabit.Api/"]

# Restore dependencies
RUN dotnet restore "src/DevHabit.Api/DevHabit.Api.csproj"

# Copy all source
COPY . .

# Build the app
WORKDIR "/src/src/DevHabit.Api"
RUN dotnet build "DevHabit.Api.csproj" -c $configuration -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "DevHabit.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "DevHabit.Api.dll"]
