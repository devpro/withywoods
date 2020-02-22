FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["samples/AspNetCoreApiSample/AspNetCoreApiSample.csproj", "samples/AspNetCoreApiSample/"]
RUN dotnet restore "samples/AspNetCoreApiSample/AspNetCoreApiSample.csproj"
COPY . .
WORKDIR "/src/samples/AspNetCoreApiSample"
RUN dotnet build "AspNetCoreApiSample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCoreApiSample.csproj" -c Release -o  /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Withywoods.AspNetCoreApiSample.dll"]
