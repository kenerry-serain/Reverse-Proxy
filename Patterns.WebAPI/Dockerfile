FROM microsoft/dotnet:2.1-sdk-alpine AS build-env
WORKDIR /app
EXPOSE 80
COPY . ./
RUN dotnet restore Patterns.WebAPI/Patterns.WebAPI.csproj
WORKDIR /app/Patterns.WebAPI/ 
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=build-env /app/Patterns.WebAPI/out . 
ENTRYPOINT ["dotnet", "Patterns.WebAPI.dll"]