namespace GinjaSoft.Text.Tests
{
  using System;
  using Xunit;


  public class StringTableBuilderTests
  {
    private static readonly string s_nl = Environment.NewLine;


    [Fact]
    public void EmptyTable()
    {
      var table = new StringTableBuilder();
      var output = table.ToString();
      Console.WriteLine(output);
      const string expectedOutput = "";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableNoBordersOrPadding()
    {
      var table = CreateSimpleTableBuilder();
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "abc" + s_nl +
        "def" + s_nl +
        "ghi";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableNoBordersCellPadding1()
    {
      var table = CreateSimpleTableBuilder().SetCellPadding(1);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "       " + s_nl +
        " a b c " + s_nl +
        "       " + s_nl +
        " d e f " + s_nl +
        "       " + s_nl +
        " g h i " + s_nl +
        "       ";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableNoBordersCellRowPadding1()
    {
      var table = CreateSimpleTableBuilder().SetCellRowPadding(1);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "   " + s_nl +
        "abc" + s_nl +
        "   " + s_nl +
        "def" + s_nl +
        "   " + s_nl +
        "ghi" + s_nl +
        "   ";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableNoBordersCellColumnPadding1()
    {
      var table = CreateSimpleTableBuilder().SetCellColumnPadding(1);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        " a b c " + s_nl +
        " d e f " + s_nl +
        " g h i ";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableNoBordersCellPadding2()
    {
      var table = CreateSimpleTableBuilder().SetCellPadding(2);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "           " + s_nl +
        "           " + s_nl +
        "  a  b  c  " + s_nl +
        "           " + s_nl +
        "           " + s_nl +
        "  d  e  f  " + s_nl +
        "           " + s_nl +
        "           " + s_nl +
        "  g  h  i  " + s_nl +
        "           " + s_nl +
        "           ";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableNoBordersInnerCellPadding1()
    {
      var table = CreateSimpleTableBuilder().SetInnerCellPadding(1);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "a b c" + s_nl +
        "     " + s_nl +
        "d e f" + s_nl +
        "     " + s_nl +
        "g h i";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableNoBordersInnerCellRowPadding1()
    {
      var table = CreateSimpleTableBuilder().SetInnerCellRowPadding(1);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "abc" + s_nl +
        "   " + s_nl +
        "def" + s_nl +
        "   " + s_nl +
        "ghi";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableNoBordersInnerCellColumnPadding1()
    {
      var table = CreateSimpleTableBuilder().SetInnerCellColumnPadding(1);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "a b c" + s_nl +
        "d e f" + s_nl +
        "g h i";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableNoBordersInnerCellPadding2()
    {
      var table = CreateSimpleTableBuilder().SetInnerCellPadding(2);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "a  b  c" + s_nl +
        "       " + s_nl +
        "       " + s_nl +
        "d  e  f" + s_nl +
        "       " + s_nl +
        "       " + s_nl +
        "g  h  i";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableNoBordersInnerCellRowPadding2()
    {
      var table = CreateSimpleTableBuilder().SetInnerCellRowPadding(2);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "abc" + s_nl +
        "   " + s_nl +
        "   " + s_nl +
        "def" + s_nl +
        "   " + s_nl +
        "   " + s_nl +
        "ghi";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableNoBordersInnerCellColumnPadding2()
    {
      var table = CreateSimpleTableBuilder().SetInnerCellColumnPadding(2);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "a  b  c" + s_nl +
        "d  e  f" + s_nl +
        "g  h  i";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableNoBordersCellPadding1InnerCellPadding1()
    {
      var table = CreateSimpleTableBuilder().SetCellPadding(1).SetInnerCellPadding(1);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
         "         " + s_nl +
         " a  b  c " + s_nl +
         "         " + s_nl +
         "         " + s_nl +
         " d  e  f " + s_nl +
         "         " + s_nl +
         "         " + s_nl +
         " g  h  i " + s_nl +
         "         ";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableBordersNoPadding()
    {
      var table = CreateSimpleTableBuilder().SetDrawBorders(true);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "+-+-+-+" + s_nl +
        "|a|b|c|" + s_nl +
        "+-+-+-+" + s_nl +
        "|d|e|f|" + s_nl +
        "+-+-+-+" + s_nl +
        "|g|h|i|" + s_nl +
        "+-+-+-+";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableBordersCellPadding1()
    {
      var table = CreateSimpleTableBuilder().SetDrawBorders(true).SetCellPadding(1);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "+---+---+---+" + s_nl +
        "|   |   |   |" + s_nl +
        "| a | b | c |" + s_nl +
        "|   |   |   |" + s_nl +
        "+---+---+---+" + s_nl +
        "|   |   |   |" + s_nl +
        "| d | e | f |" + s_nl +
        "|   |   |   |" + s_nl +
        "+---+---+---+" + s_nl +
        "|   |   |   |" + s_nl +
        "| g | h | i |" + s_nl +
        "|   |   |   |" + s_nl +
        "+---+---+---+";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void SimpleTableBordersInnerCellPadding1()
    {
      var table = CreateSimpleTableBuilder().SetDrawBorders(true).SetInnerCellPadding(1);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "+--+---+--+" + s_nl +
        "|a | b | c|" + s_nl +
        "|  |   |  |" + s_nl +
        "+--+---+--+" + s_nl +
        "|  |   |  |" + s_nl +
        "|d | e | f|" + s_nl +
        "|  |   |  |" + s_nl +
        "+--+---+--+" + s_nl +
        "|  |   |  |" + s_nl +
        "|g | h | i|" + s_nl +
        "+--+---+--+";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void ComplexTableNoBordersOrPadding()
    {
      var table = CreateComplexTableBuilder();
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "foo      foobarbazthis is a " + s_nl +
        "                  multi-line" + s_nl +
        "                  string    " + s_nl +
        "foobar   e        f         " + s_nl +
        "         ee                 " + s_nl +
        "         eee                " + s_nl +
        "         e                  " + s_nl +
        "foobarbazh        i         ";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void ComplexTableNoBordersOrPaddingMixedNewLines()
    {
      var table = CreateComplexTableBuilderWithMixedNewLines();
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "foo      foobarbazthis is a " + s_nl +
        "                  multi-line" + s_nl +
        "                  string    " + s_nl +
        "foobar   e        f         " + s_nl +
        "         ee                 " + s_nl +
        "         eee                " + s_nl +
        "         e                  " + s_nl +
        "foobarbazh        i         ";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void ComplexTableNoBordersInnerCellColumnPadding2()
    {
      var table = CreateComplexTableBuilder().SetInnerCellColumnPadding(2);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "foo        foobarbaz  this is a " + s_nl +
        "                      multi-line" + s_nl +
        "                      string    " + s_nl +
        "foobar     e          f         " + s_nl +
        "           ee                   " + s_nl +
        "           eee                  " + s_nl +
        "           e                    " + s_nl +
        "foobarbaz  h          i         ";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void ComplexTableNoBordersInnerCellColumnPadding2HAlignColumnsRight()
    {
      var table = CreateComplexTableBuilderHAlignRightColumns().SetInnerCellColumnPadding(2);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "      foo  foobarbaz   this is a" + s_nl +
        "                      multi-line" + s_nl +
        "                          string" + s_nl +
        "   foobar          e           f" + s_nl +
        "                  ee            " + s_nl +
        "                 eee            " + s_nl +
        "                   e            " + s_nl +
        "foobarbaz          h           i";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void ComplexTableNoBordersInnerCellColumnPadding2HAlignColumnsLeft()
    {
      var table = CreateComplexTableBuilderHAlignLeftColumns().SetInnerCellColumnPadding(2);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "foo        foobarbaz  this is a " + s_nl +
        "                      multi-line" + s_nl +
        "                      string    " + s_nl +
        "foobar     e          f         " + s_nl +
        "           ee                   " + s_nl +
        "           eee                  " + s_nl +
        "           e                    " + s_nl +
        "foobarbaz  h          i         ";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void ComplexTableNoBordersInnerCellColumnPadding2HAlignColumnsMixed()
    {
      var table = CreateComplexTableBuilderHAlignMixedColumns().SetInnerCellColumnPadding(2);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "      foo  foobarbaz   this is a" + s_nl +
        "                      multi-line" + s_nl +
        "                          string" + s_nl +
        "foobar             e  f         " + s_nl +
        "                  ee            " + s_nl +
        "                 eee            " + s_nl +
        "                   e            " + s_nl +
        "foobarbaz          h           i";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void ComplexTableNoBordersInnerCellColumnPadding2HAlignLeftRow()
    {
      var table = CreateComplexTableBuilderHAlignLeftRow().SetInnerCellColumnPadding(2);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "foo        foobarbaz  this is a " + s_nl +
        "                      multi-line" + s_nl +
        "                      string    " + s_nl +
        "foobar     e          f         " + s_nl +
        "           ee                   " + s_nl +
        "           eee                  " + s_nl +
        "           e                    " + s_nl +
        "foobarbaz  h          i         ";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void ComplexTableNoBordersInnerCellColumnPadding2HAlignRightRow()
    {
      var table = CreateComplexTableBuilderHAlignRightRow().SetInnerCellColumnPadding(2);
      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "foo        foobarbaz  this is a " + s_nl +
        "                      multi-line" + s_nl +
        "                      string    " + s_nl +
        "   foobar          e           f" + s_nl +
        "                  ee            " + s_nl +
        "                 eee            " + s_nl +
        "                   e            " + s_nl +
        "foobarbaz  h          i         ";
      Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void AddEmptyRow()
    {
      var table = new StringTableBuilder();
      table.AddColumn("col1");
      table.AddColumn("col2");
      table.AddColumn("col3");

      var row = table.AddRow();
      row.SetCell("col1", "a");
      row.SetCell("col2", "b");
      row.SetCell("col3", "c");

      table.AddEmptyRow();

      row = table.AddRow();
      row.SetCell("col1", "d");
      row.SetCell("col2", "e");
      row.SetCell("col3", "f");

      var output = table.ToString();
      Console.WriteLine(output);
      var expectedOutput =
        "abc" + s_nl +
        "   " + s_nl +
        "def";
      Assert.Equal(expectedOutput, output);
    }


    private static StringTableBuilder CreateSimpleTableBuilder()
    {
      var table = new StringTableBuilder();
      table.AddColumn("col1");
      table.AddColumn("col2");
      table.AddColumn("col3");

      var row = table.AddRow();
      row.SetCell("col1", "a");
      row.SetCell("col2", "b");
      row.SetCell("col3", "c");

      row = table.AddRow().SetHAlignLeft();
      row.SetCell("col1", "d");
      row.SetCell("col2", "e");
      row.SetCell("col3", "f");

      row = table.AddRow();
      row.SetCell("col1", "g");
      row.SetCell("col2", "h");
      row.SetCell("col3", "i");

      return table;
    }

    private static StringTableBuilder CreateComplexTableBuilder()
    {
      var table = new StringTableBuilder();
      table.AddColumn("col1");
      table.AddColumn("col2");
      table.AddColumn("col3");

      var row = table.AddRow();
      row.SetCell("col1", "foo");
      row.SetCell("col2", "foobarbaz");
      row.SetCell("col3", "this is a\nmulti-line\nstring");

      row = table.AddRow();
      row.SetCell("col1", "foobar");
      row.SetCell("col2", "e\nee\neee\ne");
      row.SetCell("col3", "f");

      row = table.AddRow();
      row.SetCell("col1", "foobarbaz");
      row.SetCell("col2", "h");
      row.SetCell("col3", "i");

      return table;
    }

    private static StringTableBuilder CreateComplexTableBuilderWithMixedNewLines()
    {
      var table = new StringTableBuilder();
      table.AddColumn("col1");
      table.AddColumn("col2");
      table.AddColumn("col3");

      var row = table.AddRow();
      row.SetCell("col1", "foo");
      row.SetCell("col2", "foobarbaz");
      row.SetCell("col3", "this is a\r\nmulti-line\nstring");

      row = table.AddRow();
      row.SetCell("col1", "foobar");
      row.SetCell("col2", "e\r\nee\neee\r\ne");
      row.SetCell("col3", "f");

      row = table.AddRow();
      row.SetCell("col1", "foobarbaz");
      row.SetCell("col2", "h");
      row.SetCell("col3", "i");

      return table;
    }

    private static StringTableBuilder CreateComplexTableBuilderHAlignRightColumns()
    {
      var table = new StringTableBuilder();
      table.AddColumn("col1").SetHAlignRight();
      table.AddColumn("col2").SetHAlignRight();
      table.AddColumn("col3").SetHAlignRight();

      var row = table.AddRow();
      row.SetCell("col1", "foo");
      row.SetCell("col2", "foobarbaz");
      row.SetCell("col3", "this is a\nmulti-line\nstring");

      row = table.AddRow();
      row.SetCell("col1", "foobar");
      row.SetCell("col2", "e\nee\neee\ne");
      row.SetCell("col3", "f");

      row = table.AddRow();
      row.SetCell("col1", "foobarbaz");
      row.SetCell("col2", "h");
      row.SetCell("col3", "i");

      return table;
    }

    private static StringTableBuilder CreateComplexTableBuilderHAlignRightRow()
    {
      var table = new StringTableBuilder();
      table.AddColumn("col1");
      table.AddColumn("col2");
      table.AddColumn("col3");

      var row = table.AddRow();
      row.SetCell("col1", "foo");
      row.SetCell("col2", "foobarbaz");
      row.SetCell("col3", "this is a\nmulti-line\nstring");

      row = table.AddRow().SetHAlignRight();
      row.SetCell("col1", "foobar");
      row.SetCell("col2", "e\nee\neee\ne");
      row.SetCell("col3", "f");

      row = table.AddRow();
      row.SetCell("col1", "foobarbaz");
      row.SetCell("col2", "h");
      row.SetCell("col3", "i");

      return table;
    }

    private static StringTableBuilder CreateComplexTableBuilderHAlignLeftRow()
    {
      var table = new StringTableBuilder();
      table.AddColumn("col1");
      table.AddColumn("col2");
      table.AddColumn("col3");

      var row = table.AddRow();
      row.SetCell("col1", "foo");
      row.SetCell("col2", "foobarbaz");
      row.SetCell("col3", "this is a\nmulti-line\nstring");

      row = table.AddRow().SetHAlignLeft();
      row.SetCell("col1", "foobar");
      row.SetCell("col2", "e\nee\neee\ne");
      row.SetCell("col3", "f");

      row = table.AddRow();
      row.SetCell("col1", "foobarbaz");
      row.SetCell("col2", "h");
      row.SetCell("col3", "i");

      return table;
    }

    private static StringTableBuilder CreateComplexTableBuilderHAlignLeftColumns()
    {
      var table = new StringTableBuilder();
      table.AddColumn("col1").SetHAlignLeft();
      table.AddColumn("col2").SetHAlignLeft();
      table.AddColumn("col3").SetHAlignLeft();

      var row = table.AddRow();
      row.SetCell("col1", "foo");
      row.SetCell("col2", "foobarbaz");
      row.SetCell("col3", "this is a\nmulti-line\nstring");

      row = table.AddRow();
      row.SetCell("col1", "foobar");
      row.SetCell("col2", "e\nee\neee\ne");
      row.SetCell("col3", "f");

      row = table.AddRow();
      row.SetCell("col1", "foobarbaz");
      row.SetCell("col2", "h");
      row.SetCell("col3", "i");

      return table;
    }

    private static StringTableBuilder CreateComplexTableBuilderHAlignMixedColumns()
    {
      var table = new StringTableBuilder();
      table.AddColumn("col1").SetHAlignRight();
      table.AddColumn("col2").SetHAlignRight();
      table.AddColumn("col3").SetHAlignRight();

      var row = table.AddRow();
      row.SetCell("col1", "foo");
      row.SetCell("col2", "foobarbaz");
      row.SetCell("col3", "this is a\nmulti-line\nstring");

      row = table.AddRow().SetHAlignLeft();
      row.SetCell("col1", "foobar");
      row.SetCell("col2", "e\nee\neee\ne").SetHAlignRight();
      row.SetCell("col3", "f");

      row = table.AddRow();
      row.SetCell("col1", "foobarbaz");
      row.SetCell("col2", "h");
      row.SetCell("col3", "i");

      return table;
    }
  }
}
