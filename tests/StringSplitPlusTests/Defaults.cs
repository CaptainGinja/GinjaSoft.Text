namespace GinjaSoft.Text.Tests.StringSplitPlusTests
{
  using Xunit;


  public class Defaults
  {
    [Fact]
    public void DefaultDelimiter()
    {
      // The default delimiter is ,
      var options = new StringSplitPlusOptions();
      Assert.Equal(new[] { "foo", "bar" }, "foo,bar".SplitPlus(options));
    }

    [Fact]
    public void DefaultQuotes()
    {
      // The default quote character is "
      var options = new StringSplitPlusOptions();
      Assert.Equal(new[] { "foo", "bar,baz" }, "foo,\"bar,baz\"".SplitPlus(options));
    }

    [Fact]
    public void DefaultEscapedQuotes()
    {
      // The default escaped quote character is \"
      var options = new StringSplitPlusOptions();
      Assert.Equal(new[] { "foo", "bar,baz", "bing\"bong" }, "foo,\"bar,baz\",\"bing\\\"bong\"".SplitPlus(options));
    }

    [Fact]
    public void KeepEmptyElements()
    {
      // By default we keep empty elements
      var options = new StringSplitPlusOptions().SetDelimiter(",");
      Assert.Equal(new[] { "foo", "", "bar" }, "foo,,bar".SplitPlus(options));
    }
  }
}
