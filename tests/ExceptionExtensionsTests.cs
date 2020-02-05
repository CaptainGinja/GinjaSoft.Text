namespace GinjaSoft.Text.Tests
{
  using System;
  using Xunit;
  using System.Diagnostics;
  using System.Runtime.CompilerServices;
  using System.Text.RegularExpressions;


  public class ExceptionExtensionsTests
  {
    //
    // I use RegexOptions.IgnorePatternWhitespace below so that I can spread the patterns over multiple lines, and
    // the literal newlines in the code will be ignored.  However I still want to match on spaces in each line of the
    // patterns so I have to use [ ] to represent a literal space
    //

    [Fact]
    public void Basic()
    {
      var pattern = $@"
^System.ArgumentException:[ ]exception[ ]message(\r|\r\n)
at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.ThrowException()[^\n]*(\r|\r\n)
at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.{GetCurrentMethodName()}()[^\n]*$";

      try {
        ThrowException();
      }
      catch(Exception e) {
        var output = e.ToPrettyString();
        Console.WriteLine(output);
        var match = Regex.Match(output, pattern, RegexOptions.IgnorePatternWhitespace);
        Assert.True(match.Success);
      }
    }

    [Fact]
    public void DeeperStackTrace()
    {
      var pattern = $@"
^System.ArgumentException:[ ]exception[ ]message(\r|\r\n)
at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.ThrowException()[^\n]*(\r|\r\n)
at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.ThrowExceptionWithDeeperStackTrace()[^\n]*(\r|\r\n)
at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.{GetCurrentMethodName()}()[^\n]*$";

      try {
        ThrowExceptionWithDeeperStackTrace();
      }
      catch(Exception e) {
        var output = e.ToPrettyString();
        Console.WriteLine(output);
        var match = Regex.Match(output, pattern, RegexOptions.IgnorePatternWhitespace);
        Assert.True(match.Success);
      }
    }
    
    [Fact]
    public void InnerException()
    {
      var pattern = $@"
^System.Exception:[ ]wrapper[ ]exception[ ]message(\r|\r\n)
at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.ThrowExceptionWithInnerException()[^\n]*(\r|\r\n)
at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.{GetCurrentMethodName()}()[^\n]*(\r|\r\n)
[ ][ ]>>[ ]Inner[ ]exception(\r|\r\n)
[ ][ ]System.ArgumentException:[ ]exception[ ]message(\r|\r\n)
[ ][ ]at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.ThrowException()[^\n]*(\r|\r\n)
[ ][ ]at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.ThrowExceptionWithInnerException()[^\n]*(\r|\r\n)
[ ][ ]<<[ ]Inner[ ]exception$";

      try {
        ThrowExceptionWithInnerException();
      }
      catch(Exception e) {
        var output = e.ToPrettyString();
        Console.WriteLine(output);
        var match = Regex.Match(output, pattern, RegexOptions.IgnorePatternWhitespace);
        Assert.True(match.Success);
      }
    }

    [Fact]
    public void InnerInnerException()
    {
      var pattern = $@"
^System.Exception:[ ]wrapper[ ]wrapper[ ]exception[ ]message(\r|\r\n)
at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.ThrowExceptionWithInnerInnerException()[^\n]*(\r|\r\n)
at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.{GetCurrentMethodName()}()[^\n]*(\r|\r\n)
[ ][ ]>>[ ]Inner[ ]exception(\r|\r\n)
[ ][ ]System.Exception:[ ]wrapper[ ]exception[ ]message(\r|\r\n)
[ ][ ]at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.ThrowExceptionWithInnerException()[^\n]*(\r|\r\n)
[ ][ ]at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.ThrowExceptionWithInnerInnerException()[^\n]*(\r|\r\n)
[ ][ ][ ][ ]>>[ ]Inner[ ]exception(\r|\r\n)
[ ][ ][ ][ ]System.ArgumentException:[ ]exception[ ]message(\r|\r\n)
[ ][ ][ ][ ]at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.ThrowException()[^\n]*(\r|\r\n)
[ ][ ][ ][ ]at[ ]GinjaSoft.Text.Tests.ExceptionExtensionsTests.ThrowExceptionWithInnerException()[^\n]*(\r|\r\n)
[ ][ ][ ][ ]<<[ ]Inner[ ]exception(\r|\r\n)
[ ][ ]<<[ ]Inner[ ]exception$";

      try {
        ThrowExceptionWithInnerInnerException();
      }
      catch(Exception e) {
        var output = e.ToPrettyString();
        Console.WriteLine(output);
        var match = Regex.Match(output, pattern, RegexOptions.IgnorePatternWhitespace);
        Assert.True(match.Success);
      }
    }

    [Fact]
    public void NewExceptionObject()
    {
      const string pattern = @"^System.Exception:[ ]exception[ ]message$";

      var output = new Exception("exception message").ToPrettyString();
      Console.WriteLine(output);
      var match = Regex.Match(output, pattern, RegexOptions.IgnorePatternWhitespace);
      Assert.True(match.Success);
    }


    //
    // Private methods
    //

    //
    // We have to mark these methods as not to be inlined in order to ensure that the stack trace will be as we expect 
    //

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowException()
    {
      throw new ArgumentException("exception message");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowExceptionWithDeeperStackTrace()
    {
      ThrowException();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowExceptionWithInnerException()
    {
      try {
        ThrowException();
      }
      catch(Exception e) {
        throw new Exception("wrapper exception message", e);
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowExceptionWithInnerInnerException()
    {
      try {
        ThrowExceptionWithInnerException();
      }
      catch(Exception e) {
        throw new Exception("wrapper wrapper exception message", e);
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public string GetCurrentMethodName()
    {
      var st = new StackTrace();
      var sf = st.GetFrame(1);
      return sf.GetMethod().Name;
    }
  }
}
