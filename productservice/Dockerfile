FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.16 AS base
WORKDIR /app
EXPOSE 80
RUN apk add icu-libs
ENV ASPNETCORE_URLS=http://*:80
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.16 AS build
ARG SUFFIX
WORKDIR /src
COPY ["productservice.csproj", "./"]
RUN dotnet restore "productservice.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "productservice.csproj" -c Release -o /app/build --version-suffix ${SUFFIX}

FROM build AS publish
ARG SUFFIX
RUN dotnet publish "productservice.csproj" -c Release -o /app/publish --version-suffix ${SUFFIX}

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "productservice.dll"]
