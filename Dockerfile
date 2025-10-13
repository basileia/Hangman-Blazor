FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Hangman/Hangman.csproj", "Hangman/"]
RUN dotnet restore "Hangman/Hangman.csproj"

COPY . .
RUN dotnet publish "Hangman/Hangman.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV DOTNET_RUNNING_IN_CONTAINER=true
# Expose a default port (Render supplies PORT at runtime). Clients must bind to the PORT env var.
EXPOSE 8080

# Use sh -c so the $PORT env var is expanded at container start time.
ENTRYPOINT ["sh", "-c", "export ASPNETCORE_URLS=http://+:${PORT:-8080} && dotnet Hangman.dll"]