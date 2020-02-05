namespace GinjaSoft.Text
{
  using System;
  using System.Linq;
  using System.Text;
  using System.Text.RegularExpressions;


  /// <summary>
  /// Useful string extensions
  /// </summary>
  public static class StringExtensions
  {
    //
    // Public methods
    //

    /// <summary>
    /// More direct access to string.Format
    /// </summary>
    /// <param name="s">The string template</param>
    /// <param name="args">The array of arguments to interpolate into the template string</param>
    /// <returns>The formatted string</returns>
    public static string _(this string s, params object[] args)
    {
      if(s == null) throw new ArgumentNullException();
      return string.Format(s, args);
    }

    /// <summary>
    /// A convenient test for whether a string is empty
    /// </summary>
    /// <param name="s">The string to test</param>
    /// <returns>True/False for whether the string is, or is not empty</returns>
    public static bool IsEmpty(this string s)
    {
      if(s == null) throw new ArgumentNullException();
      return s.Length == 0;
    }


    /// <summary>
    /// Create a string via repeated concatentation of a given string
    /// </summary>
    /// <param name="s">The string to repeat</param>
    /// <param name="numberOfRepetitions">The number of times to repeat the given string in the resulting string</param>
    /// <returns>The resulting string</returns>
    public static string Repeat(this string s, int numberOfRepetitions)
    {
      if(s == null) throw new ArgumentNullException();
      if(numberOfRepetitions < 1) throw new ArgumentException("numberOfRepetitions must be > 0");

      var sb = new StringBuilder();
      for(var n = 0; n < numberOfRepetitions; n++) sb.Append(s);
      return sb.ToString();
    }


    /// <summary>
    /// Split a string to a list of elements based on a spec.
    /// Supports quoted tokens containing delimiters (to ignore) and escaped quotes.
    /// Default options are used if no options are given.
    /// See StringSplitPlusOptions for more info.
    /// </summary>
    /// <param name="s">The string to split</param>
    /// <param name="options">The options object specifying how to split the string</param>
    /// <returns>An array of split elements</returns>
    /// <remarks>
    /// 
    /// </remarks>
    public static string[] SplitPlus(this string s, StringSplitPlusOptions options = null)
    {
      if(options == null) options = new StringSplitPlusOptions();
      var helper = new StringSplitPlusHelper(s, options);
      return helper.Split();
    }

    /// <summary>
    /// Prefix each line of a multi-line string with a given string
    /// </summary>
    /// <param name="s">The string who's lines are to be prefixed</param>
    /// <param name="prefix">The prefix string</param>
    /// <returns>The prefixed multi-line string</returns>
    /// /// <remarks>
    /// Both \r\n and \n are considered valid line delimiters in the input string
    /// </remarks>
    public static string PrefixLines(this string s, string prefix)
    {
      return s.ForEachLine((builder, line) => builder.Append(prefix).Append(line));
    }

    /// <summary>
    /// Postfix each line of a multi-line string with a given string
    /// </summary>
    /// <param name="s">The string who's lines are to be postfixed</param>
    /// <param name="postfix">The postfix string</param>
    /// <returns>The postfixed multi-line string</returns>
    /// /// <remarks>
    /// Both \r\n and \n are considered valid line delimiters in the input string
    /// </remarks>
    public static string PostfixLines(this string s, string postfix)
    {
      return s.ForEachLine((builder, line) => builder.Append(line).Append(postfix));
    }

    /// <summary>
    /// Apply a transformation function to each line of a multi-line string
    /// </summary>
    /// <param name="s">The string who's lines to be transformed</param>
    /// <param name="transformLineFn">The function to apply to each line</param>
    /// <returns>The transformed multi-line string</returns>
    /// <remarks>
    /// Both \r\n and \n are considered valid line delimiters in the input string
    /// </remarks>
    public static string ForEachLine(this string s, Action<StringBuilder, string> transformLineFn)
    {
      var options = new StringSplitPlusOptions().SetDelimiterList(new[] { "\r\n", "\n" });
      var lines = s.SplitPlus(options);

      var builder = new StringBuilder();

      var firstLine = true;
      foreach(var line in lines) {
        if(!firstLine) builder.Append(Environment.NewLine);
        transformLineFn(builder, line);
        firstLine = false;
      }

      return builder.ToString();
    }

    /// <summary>
    /// Convert a string to an enum by matching the string to a valid enum name.  Case insensitive.
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="text">The string to parse and interpret as an enum value</param>
    /// <returns>The resulting enum value</returns>
    /// <remarks>
    /// This just calles Enum.Parse and will throw the same exceptions
    /// </remarks>
    public static T ToEnum<T>(this string text)
    {
      var result = (T)Enum.Parse(typeof(T), text, true);
      return result;
    }

    /// <summary>
    /// Convert a string to an enum by matching the string to a valid enum name.  Case insensitive.  If the
    /// string doesn't match a valid enum name then the specified defaultValue is returned.
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="text">The string to parse and interpret as an enum value</param>
    /// <param name="defaultValue">The value to return if the string doesn't match a valid enum name</param>
    /// <returns>The resulting enum value</returns>
    public static T ToEnum<T>(this string text, T defaultValue) where T : struct
    {
      return Enum.TryParse(text, true, out T result) ? result : defaultValue;
    }

    /// <summary>
    /// Try to convert a string to an enum by matching the string to a valid enum name.  Case insensitive.
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="text">The string to parse and interpret as an enum value</param>
    /// <param name="resultValue">Output param for the resulting enum value</param>
    /// <returns>True if the string matches a known enum name, otherwise false</returns>
    public static bool TryToEnum<T>(this string text, out T resultValue) where T : struct
    {
      return Enum.TryParse(text, true, out resultValue);
    }

    /// <summary>
    /// Replace tokens in a string based on a regex pattern to match tokens and a callback function
    /// </summary>
    /// <param name="text">The string within which to match/transform tokens</param>
    /// <param name="tokenPattern">The regex pattern to match tokens</param>
    /// <param name="tokenTransformFn">The callback function through which each matched token will be replaced</param>
    /// <returns>The original string with tokens replaced</returns>
    /// <remarks>
    /// There are some requirements on the regex pattern.  It must be written with a single group specification and
    /// without a quantifer on that group.  This is so that when the pattern is used to find matches in the text we
    /// will get a collection of matches with one group and with a single capture per group. See the unit tests for
    /// example usage.
    /// </remarks>
    public static string RegexReplaceTokens(this string text,
                                            string tokenPattern,
                                            Func<string, string> tokenTransformFn)
    {
      var builder = new StringBuilder();
      var tokenEndIndex = 0;
      var regex = new Regex(tokenPattern);
      var matches = regex.Matches(text);

      foreach(Match match in matches) {
        if(match.Groups.Count != 2) {
          const string msg = "Pattern must contain one and only one group";
          throw new ArgumentException(msg, nameof(tokenPattern));
        }

        var group = match.Groups[1];
        if(group.Captures.Count != 1) {
          const string msg = "Pattern group with quantifier generated multiple captures";
          throw new ArgumentException(msg, nameof(tokenPattern));
        }

        var capture = group.Captures[0];
        
        var beforeToken = text.Substring(tokenEndIndex, capture.Index - tokenEndIndex);
        builder.Append(beforeToken);

        var token = capture.Value;
        var tokenValue = tokenTransformFn(token);
        builder.Append(tokenValue);

        tokenEndIndex = capture.Index + capture.Length;
      }

      var afterFinalToken = text.Substring(tokenEndIndex, text.Length - tokenEndIndex);
      builder.Append(afterFinalToken);

      var result = builder.ToString();
      return result;
    }
  }
}
