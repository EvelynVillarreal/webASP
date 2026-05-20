FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish ProductosMVC/ProductosMVC.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
ENV MongoDbSettings__DatabaseName=ProductosMVC
EXPOSE 8080
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "ProductosMVC.dll"]
