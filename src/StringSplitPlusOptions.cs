namespace GinjaSoft.Text
{
  using System;
  using System.Globalization;


  /// <summary>
  /// An options class to control the behavior of the SplitPlus string extension method
  /// </summary>
  public class StringSplitPlusOptions
  {
    //
    // Private data
    //

    private string[] _delimiterList = { "," };
    private string _openQuote = "\"";
    private string _closeQuote = "\"";
    private string _escapedOpenQuote = "\\\"";
    private string _equivalentOpenQuote = "\"";
    private string _escapedCloseQuote = "\\\"";
    private string _equivalentCloseQuote = "\"";
    private bool _removeEmptyElements;


    //
    // Public properties
    //

    public string[] DelimiterList => _delimiterList;
    public string OpenQuote => _openQuote;
    public string CloseQuote => _closeQuote;
    public string EscapedOpenQuote => _escapedOpenQuote;
    public string EquivalentOpenQuote => _equivalentOpenQuote;
    public string EscapedCloseQuote => _escapedCloseQuote;
    public string EquivalentCloseQuote => _equivalentCloseQuote;
    public bool RemoveEmptyElements => _removeEmptyElements;


    //
    // Public constructor
    //

    /// <summary>
    /// Constructor for SplitPlus string extension method options class.
    /// 
    /// Default options:
    ///   Delimiter=,
    ///   OpenQuote=CloseQuote="
    ///   EscapedOpenQuote=EscapedCloseQuote=(\" replaced with ")
    ///   RemoveEmptyElements=false
    /// </summary>
    public StringSplitPlusOptions() { }


    //
    // Public methods
    //

    /// <summary>
    /// Set the option to not include empty elements in the results.  Example behavior with this option set:
    ///   "a,,b" => ["a", "b"] ;
    ///   "" => [] ;
    ///   "a," => ["a"] ;
    ///   ",a" => ["a"] ;
    ///   "a" => ["a"] ;
    ///   "a,b" => ["a", "b"]
    /// </summary>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetRemoveEmptyElements()
    {
      _removeEmptyElements = true;
      return this;
    }

    /// <summary>
    /// Set the option to include empty elements in the results.  This is the default behavior.  Example behavior with
    /// this option set:
    ///   "a,,b" => ["a", "", "b"] ;
    ///   "" => [""] ;
    ///   "a," => ["a", ""] ;
    ///   ",a" => ["", "a"] ;
    ///   "a" => ["a"] ;
    ///   "a,b" => ["a", "b"] ;
    /// </summary>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetKeepEmptyElements()
    {
      _removeEmptyElements = false;
      return this;
    }

    /// <summary>
    /// Set the list of strings to interpret as a delimiter in the string to split.
    /// Examples: { "\r\n", "\n" } or { "\t", " " } ...
    /// </summary>
    /// <param name="delimiterList">The list of delimiter strings</param>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetDelimiterList(string[] delimiterList)
    {
      _delimiterList = delimiterList;
      return this;
    }

    /// <summary>
    /// Set the char to interpret as the delimiter in the string to split.
    /// Examples: ','  or ';' or '|' ...
    /// </summary>
    /// <param name="delimiter">The delimiter char</param>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetDelimiter(char delimiter)
    {
      _delimiterList = new[] { delimiter.ToString(CultureInfo.InvariantCulture) };
      return this;
    }

    /// <summary>
    /// Set the string to interpret as the delimiter in the string to split.
    /// Examples: ","  or ";" or "||" or "+++" ...
    /// </summary>
    /// <param name="delimiter">The delimiter string</param>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetDelimiter(string delimiter)
    {
      if(delimiter == null) throw new ArgumentNullException(nameof(delimiter));
      if(delimiter.IsEmpty()) throw new ArgumentException("delimiter arg cannot be empty");

      _delimiterList = new[] { delimiter };
      return this;
    }

    /// <summary>
    /// Set the character to interpret as opening a quoted section in the string to split.
    /// Examples: "  or ' or { or [ ...
    /// </summary>
    /// <param name="openQuote">The open quote char</param>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetOpenQuote(char openQuote)
    {
      _openQuote = openQuote.ToString(CultureInfo.InvariantCulture);
      return this;
    }

    /// <summary>
    /// Set the string to interpret as opening a quoted section in the string to split.
    /// Examples: "  or ' or {{ or [{ or [[[ ...
    /// </summary>
    /// <param name="openQuote">The open quote string</param>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetOpenQuote(string openQuote)
    {
      if(openQuote == null) throw new ArgumentNullException(nameof(openQuote));
      if(openQuote.IsEmpty()) throw new ArgumentException("openQuote arg cannot be empty");

      _openQuote = openQuote;
      return this;
    }

    /// <summary>
    /// Set the character to interpret as closing a quoted section in the string to split.
    /// Examples: "  or ' or } or ] ...
    /// </summary>
    /// <param name="closeQuote">The close quote char</param>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetCloseQuote(char closeQuote)
    {
      _closeQuote = closeQuote.ToString(CultureInfo.InvariantCulture);
      return this;
    }

    /// <summary>
    /// Set the string to interpret as closing a quoted section in the string to split.
    /// Examples: "  or ' or }} or }] or ]]] ...
    /// </summary>
    /// <param name="closeQuote">The close quote string</param>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetCloseQuote(string closeQuote)
    {
      if(closeQuote == null) throw new ArgumentNullException(nameof(closeQuote));
      if(closeQuote.IsEmpty()) throw new ArgumentException("closeQuote arg cannot be empty");

      _closeQuote = closeQuote;
      return this;
    }

    /// <summary>
    /// Set the char to interpret as both opening and closing a quoted section in the string to split.
    /// Examples: "  or ' or # ...
    /// </summary>
    /// <param name="quote">The quote char</param>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetQuote(char quote)
    {
      _openQuote = _closeQuote = quote.ToString(CultureInfo.InvariantCulture);
      return this;
    }

    /// <summary>
    /// Set the string to interpret as both opening and closing a quoted section in the string to split.
    /// Examples: "  or ' or # or ## or %%% ...
    /// </summary>
    /// <param name="quote">The quote string</param>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetQuote(string quote)
    {
      if(quote == null) throw new ArgumentNullException(nameof(quote));
      if(quote.IsEmpty()) throw new ArgumentException("quote arg cannot be empty");

      _openQuote = _closeQuote = quote;
      return this;
    }

    /// <summary>
    /// Set the string to interpret as an escaped open quote within the string to split.  This will result in the
    /// equivalent string being interpretted in the string.
    /// Examples: SetEscapedOpenQuote("\\[", "[") => Use \[ to represent a literal [
    /// </summary>
    /// <param name="escapedOpenQuote">The escaped open quote string</param>
    /// <param name="equivalentOpenQuote">The string to use instead of the escaped open quote</param>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetEscapedOpenQuote(string escapedOpenQuote, string equivalentOpenQuote)
    {
      if(escapedOpenQuote == null) throw new ArgumentNullException(nameof(escapedOpenQuote));
      if(equivalentOpenQuote == null) throw new ArgumentNullException(nameof(equivalentOpenQuote));
      if(escapedOpenQuote.IsEmpty()) throw new ArgumentException("escapedOpenQuote arg cannot be empty");
      if(equivalentOpenQuote.IsEmpty()) throw new ArgumentException("equivalentOpenQuote arg cannot be empty");

      _escapedOpenQuote = escapedOpenQuote;
      _equivalentOpenQuote = equivalentOpenQuote;
      return this;
    }

    /// <summary>
    /// Set the string to interpret as an escaped close quote within the string to split.  This will result in the
    /// equivalent string being interpretted in the string.
    /// Examples: SetEscapedCloseQuote("\\]", "]") => Use \] to represent a literal ]
    /// </summary>
    /// <param name="escapedCloseQuote">The escaped close quote string</param>
    /// <param name="equivalentCloseQuote">The string to use instead of the escaped close quote</param>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetEscapedCloseQuote(string escapedCloseQuote, string equivalentCloseQuote)
    {
      if(escapedCloseQuote == null) throw new ArgumentNullException(nameof(escapedCloseQuote));
      if(equivalentCloseQuote == null) throw new ArgumentNullException(nameof(equivalentCloseQuote));
      if(escapedCloseQuote.IsEmpty()) throw new ArgumentException("escapedCloseQuote arg cannot be empty");
      if(equivalentCloseQuote.IsEmpty()) throw new ArgumentException("equivalentCloseQuote arg cannot be empty");

      _escapedCloseQuote = escapedCloseQuote;
      _equivalentCloseQuote = equivalentCloseQuote;
      return this;
    }

    /// <summary>
    /// Set the string to interpret as both an escaped open quote and an escaped close quote within the string to
    /// split.  This will result in the equivalent string being interpretted in the string.
    /// Examples:
    ///   SetEscapedCloseQuote("\\\"", "\"") => Use \" to represent a literal " ;
    ///   SetEscapedCloseQuote("\"\"", "\"") => Use "" to represent a literal "
    /// </summary>
    /// <param name="escapedQuote">The escaped quote string</param>
    /// <param name="equivalentQuote">The string to use instead of the escaped quote</param>
    /// <returns>Self reference (to support chained options)</returns>
    public StringSplitPlusOptions SetEscapedQuote(string escapedQuote, string equivalentQuote)
    {
      if(escapedQuote == null) throw new ArgumentNullException(nameof(escapedQuote));
      if(equivalentQuote == null) throw new ArgumentNullException(nameof(equivalentQuote));
      if(escapedQuote.IsEmpty()) throw new ArgumentException("escapedQuote arg cannot be empty");
      if(equivalentQuote.IsEmpty()) throw new ArgumentException("equivalentQuote arg cannot be empty");

      _escapedOpenQuote = _escapedCloseQuote = escapedQuote;
      _equivalentOpenQuote = _equivalentCloseQuote = equivalentQuote;
      return this;
    }
  }
}
