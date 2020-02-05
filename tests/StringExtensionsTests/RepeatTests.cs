namespace GinjaSoft.Text.Tests.StringExtensionsTests
{
  using System;
  using Xunit;


  public class RepeatTests
  {
    [Fact]
    public void Basic()
    {
      Assert.Equal("foofoofoo", "foo".Repeat(3));
    }

    [Fact]
    public void NullString()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Repeat(null, 3));
    }

    [Fact]
    public void RepeatZeroTimes()
    {
      Assert.Throws<ArgumentException>(() => "foo".Repeat(0));
    }

    [Fact]
    public void RepeatNegativeTimes()
    {
      Assert.Throws<ArgumentException>(() => "foo".Repeat(-1));
    }
  }
}
