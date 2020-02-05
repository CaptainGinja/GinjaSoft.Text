namespace GinjaSoft.Text.Tests.StringExtensionsTests
{
  using Xunit;


  public class TryToEnumTests
  {
    private enum TestEnum
    {
      Foo
    }


    [Fact]
    public void ValidString()
    {
      var result = "foo".TryToEnum(out TestEnum outValue);
      Assert.True(result);
      Assert.Equal(TestEnum.Foo, outValue);
    }

    [Fact]
    public void InvalidString()
    {
      var result = "bing".TryToEnum(out TestEnum outValue);
      Assert.False(result);
    }
  }
}
