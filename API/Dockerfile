FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "TheGallow.Docker.sln"
RUN dotnet build "TheGallow.Docker.sln" -c Release -o /app/build
RUN dotnet publish "API/API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .


ENV ASPNETCORE_URLS=http://+:5279
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 5279  
ENTRYPOINT ["dotnet", "API.dll"]