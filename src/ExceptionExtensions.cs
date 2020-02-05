namespace GinjaSoft.Text
{
  using System;
  using System.Text;


  /// <summary>
  /// Useful Exception extensions
  /// </summary>
  public static class ExceptionExtensions
  {
    //
    // Public methods
    //

    public static string ToPrettyString(this Exception e)
    {
      var builder = new StringBuilder();
      builder.Append(e.GetType()).Append(": ").Append(e.Message);
      if(e.StackTrace != null) {
        builder.AppendLine();
        builder.Append(e.StackTrace.ForEachLine((b, line) => b.Append(line.TrimStart(' ', '\t'))));
      }
      if(e.InnerException == null) return builder.ToString();

      builder.AppendLine();
      builder.AppendLine("  >> Inner exception");
      var inner = e.InnerException.ToPrettyString();
      builder.Append(inner.PrefixLines("  "));
      builder.AppendLine();
      builder.Append("  << Inner exception");

      return builder.ToString();
    }
  }
}
