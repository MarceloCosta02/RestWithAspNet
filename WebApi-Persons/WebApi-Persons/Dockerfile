FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY WebApi-Persons/WebApi-Persons.csproj WebApi-Persons/
RUN dotnet restore WebApi-Persons/WebApi-Persons.csproj
COPY . .
WORKDIR /src/WebApi-Persons
RUN dotnet build WebApi-Persons.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebApi-Persons.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApi-Persons.dll"]