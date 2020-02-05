namespace GinjaSoft.Text.Tests.StringExtensionsTests
{
  using System;
  using Xunit;


  public class ForEachLineTests
  {
    [Fact]
    public void Basic()
    {
      const string s = "abcabc\nbcdbcd\ncdecde";
      const string template = "abxabx{0}bxdbxd{0}xdexde";
      var expectedResult = string.Format(template, Environment.NewLine);
      Assert.Equal(expectedResult, s.ForEachLine((builder, line) => builder.Append(line.Replace('c', 'x'))));
    }
  }
}
