namespace SharedModels;
public interface IHostEnvironment
{
    bool IsProduction();
    bool IsDevelopment();
}
