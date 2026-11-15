using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using SharedModels;

namespace ReportDemoComponents;

public static class Utilities
{
  public static DataTable ToDataTable(this IEnumerable<ContentMetaData> data, string name)
  {
    DataTable table = new(name);
    table.Columns.Add(nameof(ContentMetaData.Order), typeof(sbyte));
    table.Columns.Add(nameof(ContentMetaData.Title), typeof(string));
    table.Columns.Add(nameof(ContentMetaData.Description), typeof(string));
    table.Columns.Add(nameof(ContentMetaData.Author), typeof(string));
    table.Columns.Add(nameof(ContentMetaData.Slug), typeof(string));
    table.Columns.Add(nameof(ContentMetaData.PosterUrl), typeof(string));
    table.Columns.Add(nameof(ContentMetaData.ThumbnailUrl), typeof(string));
    table.Columns.Add(nameof(ContentMetaData.ContentUrl), typeof(string));
    table.Columns.Add(nameof(ContentMetaData.IconUrl), typeof(string));
    table.Columns.Add(nameof(ContentMetaData.Channel), typeof(string));
    table.Columns.Add(nameof(ContentMetaData.Type), typeof(string));
    table.Columns.Add(nameof(ContentMetaData.CreatedOn), typeof(DateTime));
    table.Columns.Add(nameof(ContentMetaData.ModifiedOn), typeof(DateTime));
    table.Columns.Add(nameof(ContentMetaData.Keywords), typeof(List<string>));
    table.Columns.Add(nameof(ContentMetaData.VideoUrl), typeof(string));

    foreach (var item in data)
    {
      DataRow row = table.NewRow();
      row[nameof(ContentMetaData.Order)] = item.Order;
      row[nameof(ContentMetaData.Title)] = item.Title;
      row[nameof(ContentMetaData.Description)] = item.Description;
      row[nameof(ContentMetaData.Author)] = item.Author;
      row[nameof(ContentMetaData.Slug)] = item.Slug;
      row[nameof(ContentMetaData.PosterUrl)] = item.PosterUrl;
      row[nameof(ContentMetaData.ThumbnailUrl)] = item.ThumbnailUrl;
      row[nameof(ContentMetaData.ContentUrl)] = item.ContentUrl;
      row[nameof(ContentMetaData.IconUrl)] = item.IconUrl;
      row[nameof(ContentMetaData.Channel)] = item.Channel;
      row[nameof(ContentMetaData.Type)] = item.Type;
      row[nameof(ContentMetaData.CreatedOn)] = item.CreatedOn;
      row[nameof(ContentMetaData.ModifiedOn)] = item.ModifiedOn;
      row[nameof(ContentMetaData.Keywords)] = item.Keywords;
      row[nameof(ContentMetaData.VideoUrl)] = item.VideoUrl ?? (object)DBNull.Value;
      table.Rows.Add(row);
    }

    return table;
  }

  [RequiresUnreferencedCode("The following members are used by TypeDescriptor.GetProperties(typoeof(T))")]
  public static DataTable ToDataTable<T>(this IEnumerable<T> data, string name)
  {
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
    DataTable table = new(name);

    foreach (PropertyDescriptor prop in properties)
      table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

    foreach (T item in data)
    {
      DataRow row = table.NewRow();

      foreach (PropertyDescriptor prop in properties)
        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

      table.Rows.Add(row);
    }

    return table;
  }
}
