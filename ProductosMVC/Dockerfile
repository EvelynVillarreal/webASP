FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ProductosMVC/ProductosMVC.csproj", "ProductosMVC/"]
RUN dotnet restore "ProductosMVC/ProductosMVC.csproj"

COPY . .
WORKDIR "/src/ProductosMVC"
RUN dotnet publish "ProductosMVC.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production
ENV MongoDbSettings__DatabaseName=ProductosMVC

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ProductosMVC.dll"]
