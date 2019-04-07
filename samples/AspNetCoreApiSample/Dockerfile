FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["samples/AspNetCoreApiSample/AspNetCoreApiSample.csproj", "samples/AspNetCoreApiSample/"]
RUN dotnet restore "samples/AspNetCoreApiSample/AspNetCoreApiSample.csproj"
COPY . .
WORKDIR "/src/samples/AspNetCoreApiSample"
RUN dotnet build "AspNetCoreApiSample.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AspNetCoreApiSample.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Withywoods.AspNetCoreApiSample.dll"]
