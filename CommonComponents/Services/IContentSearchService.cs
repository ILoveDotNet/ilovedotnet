using SharedModels;

namespace CommonComponents.Services;

public interface IContentSearchService
{
  Task WarmUpAsync(CancellationToken cancellationToken = default);
  Task<IReadOnlyList<ContentMetaData>> SearchAsync(string searchText, CancellationToken cancellationToken = default);
}
