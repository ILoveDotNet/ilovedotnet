using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCPServer.Tools;

[McpServerToolType]
public static class TestGuideTool
{
    [McpServerTool]
    [Description("Get the testing conventions and setup used in ilovedotnet.")]
    public static string GetTestGuide(
        [Description("Test type to get guidance for: 'unit', 'e2e', or 'all' (default)")]
        string type = "all") =>
        type.ToLowerInvariant() switch
        {
            "unit" => UnitTestGuide,
            "e2e"  => E2ETestGuide,
            _      => UnitTestGuide + "\n\n" + E2ETestGuide,
        };

    private const string UnitTestGuide = """
        Unit Testing in ilovedotnet:
        - Framework:   XUnit
        - Project:     UITests/UITests.csproj
        - Settings:    UITests/ilovedotnet.runsettings
        - Coverage:    XPlat Code Coverage (collected during test run)
        - Reports:     dotnet-reportgenerator-globaltool → ./Test/Report/

        Run all tests:
          dotnet test UITests/UITests.csproj --configuration Release \
            --settings UITests/ilovedotnet.runsettings \
            --verbosity normal --logger trx \
            --collect:"XPlat Code Coverage"

        Run a single test:
          dotnet test --filter Name=<TestMethodName>
        """;

    private const string E2ETestGuide = """
        End-to-End Testing in ilovedotnet:
        - Framework: Playwright
        - Project:   E2E/
        - Validates: routes, content rendering, navigation, Blazor component output

        Run:
          dotnet test E2E/

        E2E tests run against the Blazor WASM site and cover key learning path routes.
        """;
}
