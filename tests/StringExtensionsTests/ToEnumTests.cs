namespace GinjaSoft.Text.Tests.StringExtensionsTests
{
  using System;
  using Xunit;


  public class ToEnumTests
  {
    private enum TestEnum
    {
      Foo,
      Bar,
      Baz
    }


    [Fact]
    public void ValidString()
    {
      Assert.Equal(TestEnum.Foo, "foo".ToEnum<TestEnum>());
    }

    [Fact]
    public void InvalidString()
    {
      Assert.Throws<ArgumentException>(() => "bing".ToEnum<TestEnum>());
    }

    [Fact]
    public void ValidStringWithDefault()
    {
      Assert.Equal(TestEnum.Bar, "bar".ToEnum(TestEnum.Baz));
    }

    [Fact]
    public void InValidStringWithDefault()
    {
      Assert.Equal(TestEnum.Baz, "bing".ToEnum(TestEnum.Baz));
    }
  }
}
