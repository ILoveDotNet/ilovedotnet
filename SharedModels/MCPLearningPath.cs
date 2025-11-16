namespace SharedModels;

public class MCPLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(1);

    public MCPLearningPath()
    {
        FullContents =
        [
            new ContentMetaData
            {
                Order = 1,
                Title = "Add MCP to Existing .NET REST Endpoints",
                Description = "Learn how to add Model Context Protocol (MCP) capabilities to your existing ASP.NET Core REST APIs, enabling AI agents and chatbots to interact with your backend systems seamlessly.",
                Author = "Abdul Rahman",
                Slug = "add-mcp-to-existing-dotnet-rest-endpoints",
                PosterUrl = "image/blogs/mcp/add-mcp-to-existing-dotnet-rest-endpoints.webp",
                ThumbnailUrl = "image/blogs/mcp/add-mcp-to-existing-dotnet-rest-endpoints.webp",
                ContentUrl = "blogs/add-mcp-to-existing-dotnet-rest-endpoints",
                IconUrl = "image/icons/mcp.webp",
                Channel = "MCP",
                Type = "MCP",
                CreatedOn = new DateTime(2025, 12, 14, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2025, 12, 14, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["MCP", "Model Context Protocol", "REST API", "AI Integration", "ASP.NET Core", "C# SDK", "AI Agents", "Chatbots", "API Management"]
            }
        ];
    }
}
