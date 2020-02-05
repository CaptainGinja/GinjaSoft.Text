namespace GinjaSoft.Text.Tests.StringSplitPlusTests
{
  using Xunit;


  public class Quotes
  {
    //
    // To tests ...
    //   SetQuote(string)
    //   SetOpenQuote(string), SetCloseQuote(string)
    //   SetQuote(char)
    //   SetOpenQuote(char), SetCloseQuote(char)
    //   SetEscapedQuote(string, string)
    //   SetEscapedOpenQuote(string, string), SetEscapedCloseQuote(string, string)
    //

    [Fact]
    public void SetQuote()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetQuote("'");
      Assert.Equal(new[] { "foo", "bar,baz", "asdf" }, "foo,'bar,baz',asdf".SplitPlus(options));
    }

    [Fact]
    public void SetQuoteChar()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetQuote('\'');
      Assert.Equal(new[] { "foo", "bar,baz", "asdf" }, "foo,'bar,baz',asdf".SplitPlus(options));
    }

    [Fact]
    public void SetOpenAndCloseQuote()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetOpenQuote("{").SetCloseQuote("}");
      Assert.Equal(new[] { "foo", "bar,baz", "asdf" }, "foo,{bar,baz},asdf".SplitPlus(options));
    }

    [Fact]
    public void SetOpenAndCloseQuoteChar()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetOpenQuote('{').SetCloseQuote('}');
      Assert.Equal(new[] { "foo", "bar,baz", "asdf" }, "foo,{bar,baz},asdf".SplitPlus(options));
    }

    [Fact]
    public void QuoteAtStart()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetQuote("'");
      Assert.Equal(new[] { "foo,bar", "baz" }, "'foo,bar',baz".SplitPlus(options));
    }

    [Fact]
    public void QuoteAtEnd()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetQuote("'");
      Assert.Equal(new[] { "foo", "bar,baz" }, "foo,'bar,baz'".SplitPlus(options));
    }

    [Fact]
    public void QuoteAtStartAndEnd()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetQuote("'");
      Assert.Equal(new[] { "foo,bar,baz" }, "'foo,bar,baz'".SplitPlus(options));
    }

    [Fact]
    public void OpenQuoteAtStartAndCloseQuoteAtEnd()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetOpenQuote("{").SetCloseQuote("}");
      Assert.Equal(new[] { "foo,bar,baz" }, "{foo,bar,baz}".SplitPlus(options));
    }

    [Fact]
    public void MultiCharacterQuotes()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetQuote("##");
      Assert.Equal(new[] { "foo", "bar,baz" }, "foo,##bar,baz##".SplitPlus(options));
    }

    [Fact]
    public void MultiCharacterOpenAndCloseQuotes()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetOpenQuote("[[[").SetCloseQuote("]]]");
      Assert.Equal(new[] { "foo", "bar,baz" }, "foo,[[[bar,baz]]]".SplitPlus(options));
    }

    [Fact]
    public void EscapedQuotes()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetQuote("'").SetEscapedQuote("''", "'");
      Assert.Equal(new[] { "foo", "o'reilly", "bar,baz" }, "foo,o''reilly,'bar,baz'".SplitPlus(options));
    }

    [Fact]
    public void EscapedOpenAndCloseQuotes()
    {
      var options = new StringSplitPlusOptions()
        .SetDelimiter(",")
        .SetOpenQuote("[")
        .SetCloseQuote("]")
        .SetEscapedOpenQuote("[[","[")
        .SetEscapedCloseQuote("]]","]");
      Assert.Equal(new[] { "foo", "asdf[asdf]asdf", "bar,baz" }, "foo,asdf[[asdf]]asdf,[bar,baz]".SplitPlus(options));
    }

    [Fact]
    public void UnmatchedOpenCloseQuotes()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetOpenQuote("{").SetCloseQuote("}");
      Assert.Throws<StringSplitPlusException>(() => "foo,}bar,{baz}".SplitPlus(options));
    }

    [Fact]
    public void EscapedQuotesInsideQuotes()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetQuote("\"").SetEscapedQuote("\\\"", "\"");
      Assert.Equal(new[] { "foo", "bar,baz", "asdf\"\"asdf" }, "foo,\"bar,baz\",\"asdf\\\"\\\"asdf\"".SplitPlus(options));
    }

    [Fact]
    public void OpenQuotesInsideQuotes()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetOpenQuote("{").SetCloseQuote("}");
      Assert.Throws<StringSplitPlusException>(() => "foo,{bar,{baz}".SplitPlus(options));
    }
  }
}
