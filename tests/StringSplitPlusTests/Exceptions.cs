namespace GinjaSoft.Text.Tests.StringSplitPlusTests
{
  using System;
  using Xunit;


  public class Exceptions
  {
    [Fact]
    public void DelimiterCannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() => new StringSplitPlusOptions().SetDelimiter(null));
    }

    [Fact]
    public void OpenQuoteCannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() => new StringSplitPlusOptions().SetOpenQuote(null));
    }

    [Fact]
    public void CloseQuoteCannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() => new StringSplitPlusOptions().SetCloseQuote(null));
    }

    [Fact]
    public void QuoteCannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() => new StringSplitPlusOptions().SetQuote(null));
    }

    [Fact]
    public void EscapedOpenQuoteArg1CannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() => new StringSplitPlusOptions().SetEscapedOpenQuote(null, "foo"));
    }

    [Fact]
    public void EscapedOpenQuoteArg2CannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() => new StringSplitPlusOptions().SetEscapedOpenQuote("foo", null));
    }

    [Fact]
    public void EscapedCloseQuoteArg1CannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() => new StringSplitPlusOptions().SetEscapedCloseQuote(null, "foo"));
    }

    [Fact]
    public void EscapedCloseQuoteArg2CannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() => new StringSplitPlusOptions().SetEscapedCloseQuote("foo", null));
    }

    [Fact]
    public void EscapedQuoteArg1CannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() => new StringSplitPlusOptions().SetEscapedQuote(null, "foo"));
    }

    [Fact]
    public void EscapedQuoteArg2CannotBeNull()
    {
      Assert.Throws<ArgumentNullException>(() => new StringSplitPlusOptions().SetEscapedQuote("foo", null));
    }

    [Fact]
    public void DelimiterCannotBeEmptyString()
    {
      Assert.Throws<ArgumentException>(() => new StringSplitPlusOptions().SetDelimiter(""));
    }

    [Fact]
    public void OpenQuoteCannotBeEmptyString()
    {
      Assert.Throws<ArgumentException>(() => new StringSplitPlusOptions().SetOpenQuote(""));
    }

    [Fact]
    public void CloseQuoteCannotBeEmptyString()
    {
      Assert.Throws<ArgumentException>(() => new StringSplitPlusOptions().SetCloseQuote(""));
    }

    [Fact]
    public void QuoteCannotBeEmptyString()
    {
      Assert.Throws<ArgumentException>(() => new StringSplitPlusOptions().SetQuote(""));
    }

    [Fact]
    public void EscapedOpenQuoteArg1CannotBeEmptyString()
    {
      Assert.Throws<ArgumentException>(() => new StringSplitPlusOptions().SetEscapedOpenQuote("", "foo"));
    }

    [Fact]
    public void EscapedOpenQuoteArg2CannotBeEmptyString()
    {
      Assert.Throws<ArgumentException>(() => new StringSplitPlusOptions().SetEscapedOpenQuote("foo", ""));
    }

    [Fact]
    public void EscapedCloseQuoteArg1CannotBeEmptyString()
    {
      Assert.Throws<ArgumentException>(() => new StringSplitPlusOptions().SetEscapedCloseQuote("", "foo"));
    }

    [Fact]
    public void EscapedCloseQuoteArg2CannotBeEmptyString()
    {
      Assert.Throws<ArgumentException>(() => new StringSplitPlusOptions().SetEscapedCloseQuote("foo", ""));
    }

    [Fact]
    public void EscapedQuoteArg1CannotBeEmptyString()
    {
      Assert.Throws<ArgumentException>(() => new StringSplitPlusOptions().SetEscapedQuote("", "foo"));
    }

    [Fact]
    public void EscapedQuoteArg2CannotBeEmptyString()
    {
      Assert.Throws<ArgumentException>(() => new StringSplitPlusOptions().SetEscapedQuote("foo", ""));
    }
  }
}
