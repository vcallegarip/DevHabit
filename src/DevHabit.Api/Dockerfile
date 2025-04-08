FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# it documents that the container exposes these ports — but it doesn’t make the app listen on them.
EXPOSE 8030
EXPOSE 8031

ENV ASPNETCORE_URLS=http://+:8030

USER app

# This stage is used to build the service project
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/DevHabit.Api/DevHabit.Api.csproj", "src/DevHabit.Api/"]
RUN dotnet restore "src/DevHabit.Api/DevHabit.Api.csproj"
COPY . .
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

USER root
RUN apt-get update && apt-get install -y curl unzip && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v 17.0.10712.2 -l /vsdbg
USER app