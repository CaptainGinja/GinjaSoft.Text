namespace GinjaSoft.Text.Tests.StringExtensionsTests
{
  using System;
  using Xunit;


  public class IsEmptyTests
  {
    [Fact]
    public void EmptyString()
    {
      Assert.True("".IsEmpty());
    }

    [Fact]
    public void NonEmptyString()
    {
      Assert.False("I am not empty".IsEmpty());
    }

    [Fact]
    public void NullString()
    {
      Assert.Throws<ArgumentNullException>(() => ((string)null).IsEmpty());
    }
  }
}
