FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TestVK.API/TestVK.API.csproj", "TestVK.API/"]
RUN dotnet restore "TestVK.API/TestVK.API.csproj"
COPY . .
WORKDIR "/src/TestVK.API"
RUN dotnet build "TestVK.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestVK.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestVK.API.dll"]
