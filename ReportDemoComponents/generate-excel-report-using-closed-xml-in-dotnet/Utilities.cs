using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace ReportDemoComponents;

public static class Utilities
{
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
