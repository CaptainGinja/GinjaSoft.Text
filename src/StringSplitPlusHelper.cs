namespace GinjaSoft.Text
{
  using System.Collections.Generic;
  using System.Text;


  internal class StringSplitPlusHelper
  {
    //
    // Private data
    //

    private readonly string _s;
    private readonly StringSplitPlusOptions _options;
    private readonly StringBuilder _elementBuilder = new StringBuilder();
    private int _index;
    private int _tokenStartIndex;
    private int _tokenEndIndex;
    private bool _inQuotes;
    private QuoteType _quoteType = QuoteType.None;
    private bool _endOfStringReached;


    //
    // Private types
    //

    private enum TokenType
    {
      Delimiter,
      OpenQuote,
      CloseQuote,
      EscapedOpenQuote,
      EscapedCloseQuote,
      Character,
      EndOfString
    }

    private enum QuoteType
    {
      None,
      Open,
      Close,
      OpenClose
    }


    //
    // Public constructor
    //

    public StringSplitPlusHelper(string s, StringSplitPlusOptions options)
    {
      _s = s;
      _options = options;
    }


    //
    // Public methods
    //

    public string[] Split()
    {
      var list = new List<string>();

      while(GetNextElement(out var element)) {
        if(_options.RemoveEmptyElements && element.Length == 0) continue;
        list.Add(element);
      }

      return list.ToArray();
    }


    //
    // Private methods
    //
    
    //
    // Get the type of the next token
    // Update _tokenEndIndex and _quoteType
    //
    private TokenType GetNextTokenType()
    {
      if(_tokenStartIndex == _s.Length) return TokenType.EndOfString;

      if(IsDelimiterToken()) return TokenType.Delimiter;

      if(IsEscapedQuoteToken()) {
        // ReSharper disable once SwitchStatementMissingSomeCases
        switch(_quoteType) {
        case QuoteType.Open:
          return TokenType.EscapedOpenQuote;
        case QuoteType.Close:
          return TokenType.EscapedCloseQuote;
        case QuoteType.OpenClose:
          return _inQuotes ? TokenType.EscapedCloseQuote : TokenType.EscapedOpenQuote;
        default:
          throw new StringSplitPlusException("Unknown QuoteType");
        }
      }

      if(IsQuoteToken()) {
        // ReSharper disable once SwitchStatementMissingSomeCases
        switch(_quoteType) {
        case QuoteType.Open:
          if(_inQuotes) throw new StringSplitPlusException("Open quote found inside quotes");
          return TokenType.OpenQuote;
        case QuoteType.Close:
          if(!_inQuotes) throw new StringSplitPlusException("Close quote found outside of quotes");
          return TokenType.CloseQuote;
        case QuoteType.OpenClose:
          return _inQuotes ? TokenType.CloseQuote : TokenType.OpenQuote;
        default:
          throw new StringSplitPlusException("Unknown QuoteType");
        }
      }

      ++_tokenEndIndex;
      return TokenType.Character;
    }

    //
    // Get the next element from the string according to the current options
    // Update _index, _tokenStartIndex, _tokenEndIndex _inQuotes, _quoteType and _endOfStringReached
    //
    private bool GetNextElement(out string element)
    {
      element = null;
      if(_endOfStringReached) return false;

      var done = false;
      var elementStartIndex = _index;

      while(!done) {
        string extraToAppend = null;
        var tokenType = GetNextTokenType();
        if(tokenType == TokenType.Character) {
          ++_tokenStartIndex;
          continue;
        }

        // ReSharper disable once SwitchStatementMissingSomeCases
        switch(tokenType) {
        case TokenType.Delimiter:
          done = true;
          break;
        case TokenType.OpenQuote:
          _inQuotes = true;
          break;
        case TokenType.CloseQuote:
          _inQuotes = false;
          break;
        case TokenType.EscapedOpenQuote:
          extraToAppend = _options.EquivalentOpenQuote;
          break;
        case TokenType.EscapedCloseQuote:
          extraToAppend = _options.EquivalentCloseQuote;
          break;
        case TokenType.EndOfString:
          _endOfStringReached = true;
          done = true;
          break;
        default:
          throw new StringSplitPlusException("Unknown TokenType");
        }

        _elementBuilder.Append(_s.Substring(elementStartIndex, _tokenStartIndex - elementStartIndex));
        if(extraToAppend != null) _elementBuilder.Append(extraToAppend);
        elementStartIndex = _tokenStartIndex = _tokenEndIndex;
      }

      _index = _tokenEndIndex;
      element = _elementBuilder.ToString();
      _elementBuilder.Clear();
      return true;
    }


    //
    // Is there a delimiter token starting at _s[_tokenStartIndex]?
    // If yes then set _tokenEndIndex to the next character after the token.
    //
    private bool IsDelimiterToken()
    {
      var delimiterList = _options.DelimiterList;

      foreach(var delimiter in delimiterList) {
        if(string.Compare(_s, _tokenStartIndex, delimiter, 0, delimiter.Length) != 0) continue;

        // Found delimiter.  Only consider it a real delimiter if we are not currently in quotes.
        if(_inQuotes) continue;

        _tokenEndIndex += delimiter.Length;
        return true;
      }

      return false;
    }

    //
    // Is there a quote token starting at _s[_tokenStartIndex]?
    // If yes then set _quoteType to the appropriate type and set _tokenEndIndex to the next character after the token.
    //
    private bool IsQuoteToken()
    {
      var openQuote = _options.OpenQuote;
      var closeQuote = _options.CloseQuote;

      if(string.Compare(_s, _tokenStartIndex, openQuote, 0, openQuote.Length) == 0) {
        // Found open quote
        _tokenEndIndex += openQuote.Length;
        // If the open quote and close quote strings are the same then this could actually be an open or a close quote
        _quoteType = openQuote == closeQuote ? QuoteType.OpenClose : QuoteType.Open;
        return true;
      }

      if(string.Compare(_s, _tokenStartIndex, closeQuote, 0, closeQuote.Length) == 0) {
        // Found close quote
        _tokenEndIndex += closeQuote.Length;
        _quoteType = QuoteType.Close;
        return true;
      }

      _quoteType = QuoteType.None;
      return false;
    }

    //
    // Is there an escaped quote token starting at _s[_tokenStartIndex]?
    // If yes then set _quoteType to the appropriate type and set _tokenEndIndex to the next character after the token.
    //
    private bool IsEscapedQuoteToken()
    {
      var escapedOpenQuote = _options.EscapedOpenQuote;
      var escapedCloseQuote = _options.EscapedCloseQuote;

      if(string.Compare(_s, _tokenStartIndex, escapedOpenQuote, 0, escapedOpenQuote.Length) == 0) {
        // Found escaped open quote
        _tokenEndIndex += escapedOpenQuote.Length;
        // If the open quote and close quote strings are the same then this could actually be an open or a close quote
        _quoteType = escapedOpenQuote == escapedCloseQuote ? QuoteType.OpenClose : QuoteType.Open;
        return true;
      }

      if(string.Compare(_s, _tokenStartIndex, escapedCloseQuote, 0, escapedCloseQuote.Length) == 0) {
        // Found escaped close quote
        _tokenEndIndex += escapedCloseQuote.Length;
        _quoteType = QuoteType.Close;
        return true;
      }

      _quoteType = QuoteType.None;
      return false;
    }
  }
}
