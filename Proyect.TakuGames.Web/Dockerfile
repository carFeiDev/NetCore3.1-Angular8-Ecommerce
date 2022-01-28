#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Proyect.TakuGames.Web/Proyect.TakuGames.Web.csproj", "Proyect.TakuGames.Web/"]
COPY ["Project.TakuGames.Business/Project.TakuGames.Business.csproj", "Project.TakuGames.Business/"]
COPY ["Project.TakuGames.Dal/Project.TakuGames.Dal.csproj", "Project.TakuGames.Dal/"]
COPY ["Project.TakuGames.Model/Project.TakuGames.Model.csproj", "Project.TakuGames.Model/"]
RUN dotnet restore "Proyect.TakuGames.Web/Proyect.TakuGames.Web.csproj"
COPY . .
WORKDIR "/src/Proyect.TakuGames.Web"
RUN dotnet build "Proyect.TakuGames.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Proyect.TakuGames.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Proyect.TakuGames.Web.dll"]