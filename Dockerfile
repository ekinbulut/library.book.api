FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ARG USERNAME
ARG PASSWORD
WORKDIR /app
COPY . .

RUN dotnet nuget add source https://ci.appveyor.com/nuget/ekinbulut --name appveyor --username $USERNAME --password $PASSWORD --store-password-in-clear-text
RUN dotnet restore --disable-parallel
RUN  dotnet build -c Release --no-restore -o output


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.12-alpine3.12 AS runtime
WORKDIR /app
COPY --from=build /app/output ./

EXPOSE 8091

ENTRYPOINT ["dotnet", "Library.Book.Api.dll"]