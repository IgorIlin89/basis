# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OnlineShopWeb/OnlineShopWeb.csproj", "OnlineShopWeb/"]
COPY ["OnlineShopWeb.Application/OnlineShopWeb.Application.csproj", "OnlineShopWeb.Application/"]
COPY ["GrpcAdapter/GrpcAdapter.csproj", "GrpcAdapter/"]
COPY ["Utility/Utility.csproj", "Utility/"]
COPY ["OnlineShopWeb.Domain/OnlineShopWeb.Domain.csproj", "OnlineShopWeb.Domain/"]
COPY ["OnlineShopWeb.NServiceBus.Messages/OnlineShopWeb.Messages.csproj", "OnlineShopWeb.NServiceBus.Messages/"]
COPY ["ProductCouponAdapter/ProductCouponAdapter.csproj", "ProductCouponAdapter/"]
COPY ["TransactionAdapter/TransactionAdapter.csproj", "TransactionAdapter/"]
COPY ["UserAdapter/UserAdapter.csproj", "UserAdapter/"]
RUN dotnet restore "./OnlineShopWeb/OnlineShopWeb.csproj"
COPY . .
WORKDIR "/src/OnlineShopWeb"
RUN dotnet build "./OnlineShopWeb.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnlineShopWeb.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineShopWeb.dll"]