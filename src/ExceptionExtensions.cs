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

    public static string ToPrettyString(this Exception e, bool types = true, bool stackTrace = true)
    {
      var builder = new StringBuilder();
      if(types) builder.Append(e.GetType()).Append(": ");
      builder.Append(e.Message);
      if(e.StackTrace != null && stackTrace) {
        builder.AppendLine();
        builder.Append(e.StackTrace.ForEachLine((b, line) =>
        {
          // Remove source code file/line references - Too much info and too messy
          var index = line.IndexOf(" in ");
          if(index > 0) line = line.Substring(0, index);
          b.Append(line.TrimStart(' ', '\t'));
        }));
      }
      if(e.InnerException == null) return builder.ToString();

      builder.AppendLine();
      var inner = e.InnerException.ToPrettyString(types, stackTrace);
      builder.Append(inner.PrefixLines("  "));

      return builder.ToString();
    }
  }
}
