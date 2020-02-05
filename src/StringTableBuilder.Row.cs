namespace GinjaSoft.Text
{
  using System;
  using System.Collections.Generic;


  public partial class StringTableBuilder
  {
    public class Row
    {
      //
      // Private immutable data
      //
      private readonly Dictionary<string, Cell> _columnValues = new Dictionary<string, Cell>();
      private readonly StringTableBuilder _stringTableBuilder;


      //
      // Private mutable data
      //
      private int _maxHeight;
      private HAlign _hAlign;
      private VAlign _vAlign;


      //
      // Public constructor
      // TODO:  Should this be internal?  Users call AddRow() on StringTableBuilder.  Only AddRow() calls new Row().
      //
      public Row(StringTableBuilder stringTableBuilder)
      {
        _maxHeight = 1;
        _stringTableBuilder = stringTableBuilder;
        _hAlign = HAlign.Default;
        _vAlign = VAlign.Default;
      }

      //
      // Public (read only) properties
      //
      public HAlign HAlign => _hAlign;
      public VAlign VAlign => _vAlign;


      //
      // Public methods
      //

      public Row SetHAlignLeft() { return SetHAlign(HAlign.Left); }
      public Row SetHAlignRight() { return SetHAlign(HAlign.Right); }
      public Row SetVAlignTop() { return SetVAlign(VAlign.Top); }
      public Row SetVAlignBottom() { return SetVAlign(VAlign.Bottom); }

      public Cell SetCell(string columnName, string value)
      {
        var column = _stringTableBuilder._columnsByName[columnName];
        var lines = value.Replace("\r\n", "\n").Split('\n');
        _maxHeight = Math.Max(MaxHeight, lines.Length);
        foreach(var elem in lines)
          column.MaxWidth = Math.Max(column.MaxWidth, elem.Length);
        var cell = new Cell(lines);
        _columnValues[columnName] = cell;
        return cell;
      }


      //
      // Internal (read only) properties
      //
      internal int MaxHeight => _maxHeight;


      //
      // Internal methods
      //
      internal Cell GetCell(string columnName)
      {
        return _columnValues[columnName];
      }


      //
      // Private methods
      //
      private Row SetHAlign(HAlign hAlign) { _hAlign = hAlign; return this; }
      private Row SetVAlign(VAlign vAlign) { _vAlign = vAlign; return this; }
    }
  }
}
