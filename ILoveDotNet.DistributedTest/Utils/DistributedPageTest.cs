namespace ILoveDotNet.DistributedTest.Utils;

public abstract class DistributedPageTest : PageTest
{
  public override async ValueTask InitializeAsync()
  {
    await base.InitializeAsync();
    SetDefaultExpectTimeout(30_000);
    Context.SetDefaultTimeout(60_000);
    Context.SetDefaultNavigationTimeout(60_000);
  }
}
