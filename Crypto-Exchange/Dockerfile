# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /backend

# Copy the solution file
COPY Backend/*.csproj ./Backend/
COPY Backend/test_binance_api.sln ./Backend/
COPY User.Mailing.Service/Mailing.Service.csproj ./User.Mailing.Service/

# RUN ls -R
# Restore dependencies
RUN dotnet restore Backend/test_binance_api.csproj
RUN dotnet restore User.Mailing.Service/Mailing.Service.csproj

# Publish individual projects
COPY Backend/ ./Backend
COPY User.Mailing.Service/ ./User.Mailing.Service/
COPY CryptoPredictTestProject/ ./CryptoPredictTestProject/

RUN dotnet publish Backend/test_binance_api.csproj -c release -o /backend/out
RUN dotnet publish User.Mailing.Service/Mailing.Service.csproj -c release -o /backend/out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /backend
COPY --from=build /backend/out .

# Set environment variables
ENV ASPNETCORE_URLS="http://+:5009" \
    ASPNETCORE_ENVIRONMENT=Development

EXPOSE 5009
ENTRYPOINT ["dotnet", "test_binance_api.dll"]
