namespace GinjaSoft.Text
{
  using System;


  /// <summary>
  /// A type for exceptions thrown by the SplitPlus string extension method
  /// </summary>
  public class StringSplitPlusException : Exception
  {
    public StringSplitPlusException(string msg) : base(msg) { }
  }
}
