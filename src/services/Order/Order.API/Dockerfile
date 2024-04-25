FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src
EXPOSE 80

#copy all .csproj files and restore as distinct layers.
COPY RabbitMq_ASPNET.sln RabbitMq_ASPNET.sln

#get all services .csproj fiels
COPY . .
#Restore package deps
COPY . .
WORKDIR "/src/src/services/Order/Order.API"
RUN dotnet build "./Order.API.csproj" -c Release -o /app/src/out

#Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet",  "Order.API.dll"]
