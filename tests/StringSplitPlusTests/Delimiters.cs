namespace GinjaSoft.Text.Tests.StringSplitPlusTests
{
  using Xunit;


  public class Delimiters
  {
    [Fact]
    public void NoDelimiters()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",");
      Assert.Equal(new[] { "foo" }, "foo".SplitPlus(options));
    }

    [Fact]
    public void EmptyStringRemoveEmptyElements()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetRemoveEmptyElements();
      Assert.Equal(new string[] { }, "".SplitPlus(options));
    }

    [Fact]
    public void EmptyStringKeepEmptyElements()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetKeepEmptyElements();
      Assert.Equal(new [] { "" }, "".SplitPlus(options));
    }

    [Fact]
    public void SingleDelimiterRemoveEmptyElements()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetRemoveEmptyElements();
      Assert.Equal(new string[] { }, ",".SplitPlus(options));
    }

    [Fact]
    public void SingleDelimiterKeepEmptyElements()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetKeepEmptyElements();
      Assert.Equal(new [] { "", "" }, ",".SplitPlus(options));
    }

    [Fact]
    public void MultipleElements()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",");
      Assert.Equal(new[] { "foo", "bar", "baz" }, "foo,bar,baz".SplitPlus(options));
    }

    [Fact]
    public void CharDelimiter()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(',');
      Assert.Equal(new[] { "foo", "bar", "baz" }, "foo,bar,baz".SplitPlus(options));
    }

    [Fact]
    public void MultiCharacterDelimiter()
    {
      var options = new StringSplitPlusOptions().SetDelimiter("||");
      Assert.Equal(new[] { "foo", "bar|baz" }, "foo||bar|baz".SplitPlus(options));
    }

    [Fact]
    public void ConsecutiveDelimitersRemoveEmptyElements()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetRemoveEmptyElements();
      Assert.Equal(new[] { "foo", "bar" }, "foo,,bar".SplitPlus(options));
    }

    [Fact]
    public void ConsecutiveDelimitersKeepEmptyElements()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetKeepEmptyElements();
      Assert.Equal(new[] { "foo", "", "bar" }, "foo,,bar".SplitPlus(options));
    }

    [Fact]
    public void DelimiterAtStartRemoveEmptyElements()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetRemoveEmptyElements();
      Assert.Equal(new[] { "bar", "baz" }, ",bar,baz".SplitPlus(options));
    }

    [Fact]
    public void DelimiterAtEndRemoveEmptyElements()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetRemoveEmptyElements();
      Assert.Equal(new[] { "foo", "bar" }, "foo,bar,".SplitPlus(options));
    }

    [Fact]
    public void DelimiterAtStartKeepEmptyElements()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetKeepEmptyElements();
      Assert.Equal(new[] { "", "bar", "baz" }, ",bar,baz".SplitPlus(options));
    }

    [Fact]
    public void DelimiterAtEndKeepEmptyElements()
    {
      var options = new StringSplitPlusOptions().SetDelimiter(",").SetKeepEmptyElements();
      Assert.Equal(new[] { "foo", "bar", "" }, "foo,bar,".SplitPlus(options));
    }

    [Fact]
    public void DelimiterList()
    {
      var options = new StringSplitPlusOptions().SetDelimiterList(new [] { ",", ";" });
      Assert.Equal(new [] { "foo", "bar", "baz" }, "foo,bar;baz".SplitPlus(options));
    }

    [Fact]
    public void DelimiterListDifferentLengthDelimiters()
    {
      var options = new StringSplitPlusOptions().SetDelimiterList(new [] { "\r\n", "\n" });
      Assert.Equal(new [] { "foo", "bar", "baz", "" }, "foo\r\nbar\nbaz\n".SplitPlus(options));
    }
  }
}
