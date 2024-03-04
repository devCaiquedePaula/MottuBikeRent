#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BackEnd.Api/BackEnd.Api.csproj", "BackEnd.Api/"]
COPY ["BackEnd.CrossCutting/BackEnd.CrossCutting.csproj", "BackEnd.CrossCutting/"]
COPY ["BackEnd.Domain/BackEnd.Domain.csproj", "BackEnd.Domain/"]
COPY ["BackEnd.Application/BackEnd.Application.csproj", "BackEnd.Application/"]
COPY ["BackEnd.Infra.Data.EF/BackEnd.Infra.Data.EF.csproj", "BackEnd.Infra.Data.EF/"]
RUN dotnet restore "BackEnd.Api/BackEnd.Api.csproj"
COPY . .
WORKDIR "/src/BackEnd.Api"
RUN dotnet build "BackEnd.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackEnd.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackEnd.Api.dll"]