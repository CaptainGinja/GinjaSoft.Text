namespace GinjaSoft.Text.Tests.StringExtensionsTests
{
  using System;
  using Xunit;


  public class PostfixLinesTests
  {
    [Fact]
    public void MultipleWindowsNewlinesWithNoNewlineAtEnd()
    {
      const string s = "This is line 1\r\nThis is line 2\r\nThis is line 3";
      const string template = "This is line 1  {0}This is line 2  {0}This is line 3  ";
      var expectedResult = string.Format(template, Environment.NewLine);
      Assert.Equal(expectedResult, s.PostfixLines("  "));
    }

    [Fact]
    public void MultipleUnixNewlinesWithNoNewlineAtEnd()
    {
      const string s = "This is line 1\nThis is line 2\nThis is line 3";
      const string template = "This is line 1  {0}This is line 2  {0}This is line 3  ";
      var expectedResult = string.Format(template, Environment.NewLine);
      Assert.Equal(expectedResult, s.PostfixLines("  "));
    }

    [Fact]
    public void MultipleWindowsNewlinesWithNewlineAtEnd()
    {
      const string s = "This is line 1\r\nThis is line 2\r\nThis is line 3\r\n";
      const string template = "This is line 1  {0}This is line 2  {0}This is line 3  {0}  ";
      var expectedResult = string.Format(template, Environment.NewLine);
      Assert.Equal(expectedResult, s.PostfixLines("  "));
    }

    [Fact]
    public void MultipleUnixNewlinesWithNewlineAtEnd()
    {
      const string s = "This is line 1\nThis is line 2\nThis is line 3\n";
      const string template = "This is line 1  {0}This is line 2  {0}This is line 3  {0}  ";
      var expectedResult = string.Format(template, Environment.NewLine);
      Assert.Equal(expectedResult, s.PostfixLines("  "));
    }

    [Fact]
    public void MixedNewlines()
    {
      const string s = "This is line 1\r\nThis is line 2\nThis is line 3";
      const string template = "This is line 1  {0}This is line 2  {0}This is line 3  ";
      var expectedResult = string.Format(template, Environment.NewLine);
      Assert.Equal(expectedResult, s.PostfixLines("  "));
    }

    [Fact]
    public void NoNewlines()
    {
      const string s = "This is line 1";
      const string template = "This is line 1  ";
      var expectedResult = string.Format(template);
      Assert.Equal(expectedResult, s.PostfixLines("  "));
    }

    [Fact]
    public void SingleNewlineAtEnd()
    {
      const string s = "This is line 1\n";
      const string template = "This is line 1  {0}  ";
      var expectedResult = string.Format(template, Environment.NewLine);
      Assert.Equal(expectedResult, s.PostfixLines("  "));
    }

    [Fact]
    public void NewlineAtStart()
    {
      const string s = "\nThis is line 1";
      const string template = "  {0}This is line 1  ";
      var expectedResult = string.Format(template, Environment.NewLine);
      Assert.Equal(expectedResult, s.PostfixLines("  "));
    }

    [Fact]
    public void NewlineAtStartAndEnd()
    {
      const string s = "\nThis is line 1\n";
      const string template = "  {0}This is line 1  {0}  ";
      var expectedResult = string.Format(template, Environment.NewLine);
      Assert.Equal(expectedResult, s.PostfixLines("  "));
    }

    [Fact]
    public void EmptyString()
    {
      const string s = "";
      const string template = "  ";
      var expectedResult = string.Format(template);
      Assert.Equal(expectedResult, s.PostfixLines("  "));
    }

    [Fact]
    public void SingleNewline()
    {
      const string s = "\n";
      const string template = "  {0}  ";
      var expectedResult = string.Format(template, Environment.NewLine);
      Assert.Equal(expectedResult, s.PostfixLines("  "));
    }
  }
}
