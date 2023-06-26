# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# Copy the project files
COPY . .
RUN dotnet restore "./MyDartsPredictor.Api/MyDartsPredictor.Api.csproj"
CMD dotnet ef database update

RUN dotnet publish "./MyDartsPredictor.Api/MyDartsPredictor.Api.csproj" -c release -o /app --no-restore

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./


# Expose the port
EXPOSE 5000

# Start the application
ENTRYPOINT ["dotnet", "MyDartsPredictor.Api.dll"]
