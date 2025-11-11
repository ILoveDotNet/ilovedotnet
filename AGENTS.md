## Lint, Build and Test

- **Lint:** `dotnet format --verbosity quiet whitespace --folder`
- **Build:** `dotnet build ILoveDotNet.sln`
- **Test:** `dotnet test`. To run a single test, use `--filter Name=<test_name>`.
  - You should rarely need to run all tests except after larger changes, usually running the specific test class or method you are working on is sufficient.