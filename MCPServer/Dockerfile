# Stage 1 – generate knowledge-base.json from source razor files
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS generate-kb
WORKDIR /repo
COPY . .
RUN dotnet run --project ContentExtractor/ContentExtractor.csproj -- /repo /repo/MCPServer/Data/knowledge-base.json

# Stage 2 – publish the MCP server
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /repo
COPY . .
COPY --from=generate-kb /repo/MCPServer/Data/knowledge-base.json MCPServer/Data/knowledge-base.json
RUN dotnet publish MCPServer/MCPServer.csproj -c Release -o /app/out

# Stage 3 – runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "MCPServer.dll"]
