namespace SharedModels;
public interface IHostEnvironment
{
  bool IsProduction();
  bool IsPrerendering();
  bool IsDevelopment();
}
