using SharedModels;

namespace CommonComponents.Services;

public interface IContentSearchService
{
  Task<IReadOnlyList<ContentMetaData>> SearchAsync(string searchText, CancellationToken cancellationToken = default);
}
