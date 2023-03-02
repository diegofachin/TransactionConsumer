#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TransactionConsumer/TransactionConsumer.csproj", "TransactionConsumer/"]
RUN dotnet restore "TransactionConsumer/TransactionConsumer.csproj"
COPY . .
WORKDIR "/src/TransactionConsumer"
RUN dotnet build "TransactionConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransactionConsumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionConsumer.dll"]