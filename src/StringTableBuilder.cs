namespace GinjaSoft.Text
{
  using System;
  using System.Collections.Generic;
  using System.Text;


  public partial class StringTableBuilder
  { 
    //
    // Private immutable data
    //
    private readonly List<Column> _columns = new List<Column>();
    private readonly Dictionary<string, Column> _columnsByName = new Dictionary<string, Column>();
    private readonly List<Row> _rows = new List<Row>();


    //
    // Private mutable data
    //
    private bool _drawCellBorders;
    private int _cellRowPadding;
    private int _cellColumnPadding;
    private int _innerCellRowPadding;
    private int _innerCellColumnPadding;
   

    //
    // Public enums
    //
    public enum HAlign { Default, Left, Right };
    public enum VAlign { Default, Top, Bottom };

    
    //
    // Public constructor
    //
    public StringTableBuilder()
    {
      _drawCellBorders = false;
      _cellRowPadding = 0;
      _cellColumnPadding = 0;
      _innerCellRowPadding = 0;
      _innerCellColumnPadding = 0;
    }


    //
    // Public methods
    //

    public Column AddColumn(string name)
    {
      var column = new Column(name);
      _columns.Add(column);
      _columnsByName.Add(name, column);
      return column;
    }

    public Row AddRow()
    {
      var row = new Row(this);
      _rows.Add(row);
      return row;
    }

    public Row AddEmptyRow()
    {
      var row = new Row(this);
      foreach(var col in _columns) row.SetCell(col.Name, "");
      _rows.Add(row);
      return row;
    }

    public StringTableBuilder SetCellPadding(int padding)
    {
      _cellRowPadding = _cellColumnPadding = padding;
      return this;
    }

    public StringTableBuilder SetInnerCellPadding(int padding)
    {
      _innerCellRowPadding = _innerCellColumnPadding = padding;
      return this;
    }

    public StringTableBuilder SetCellRowPadding(int padding)
    {
      _cellRowPadding = padding;
      return this;
    }

    public StringTableBuilder SetCellColumnPadding(int padding)
    {
      _cellColumnPadding = padding;
      return this;
    }

    public StringTableBuilder SetInnerCellRowPadding(int padding)
    {
      _innerCellRowPadding = padding;
      return this;
    }

    public StringTableBuilder SetInnerCellColumnPadding(int padding)
    {
      _innerCellColumnPadding = padding;
      return this;
    }

    public StringTableBuilder SetDrawBorders(bool value)
    {
      _drawCellBorders = value;
      return this;
    }

    public override string ToString()
    {
      if(_rows.Count == 0 || _columns.Count == 0) return "";

      var tableBuilder = new StringBuilder();
      DrawTopBorderAndCellPadding(tableBuilder);

      var firstRow = true;
      foreach(var row in _rows) {
        if(!firstRow) DrawInnerBorderAndCellPadding(tableBuilder);
        DrawRow(tableBuilder, '|', ' ', row);
        firstRow = false;
      }

      DrawBottomBorderAndCellPadding(tableBuilder);

      var len = Environment.NewLine.Length;
      tableBuilder.Remove(tableBuilder.Length - len, len);  // Remove closing newline from the last row
      return tableBuilder.ToString();
    }


    //
    // Private methods
    //

    private void DrawTopBorderAndCellPadding(StringBuilder tableBuilder)
    {
      if(_drawCellBorders)
        DrawBorderRow(tableBuilder);
      for(var n = 0; n < _cellRowPadding; ++n)
        DrawPaddingRow(tableBuilder);
    }

    private void DrawBottomBorderAndCellPadding(StringBuilder tableBuilder)
    {
      for(var n = 0; n < _cellRowPadding; ++n)
        DrawPaddingRow(tableBuilder);
      if(_drawCellBorders)
        DrawBorderRow(tableBuilder);
    }

    private void DrawInnerBorderAndCellPadding(StringBuilder tableBuilder)
    {
      for(var n = 0; n < _cellRowPadding + _innerCellRowPadding; ++n)
        DrawPaddingRow(tableBuilder);
      if(!_drawCellBorders) return;
      DrawBorderRow(tableBuilder);
      for(var n = 0; n < _cellRowPadding + _innerCellRowPadding; ++n)
        DrawPaddingRow(tableBuilder);
    }

    private void DrawBorderRow(StringBuilder tableBuilder)
    {
      DrawRow(tableBuilder, '+', '-', null);
    }

    private void DrawPaddingRow(StringBuilder tableBuilder)
    {
      DrawRow(tableBuilder, '|', ' ', null);
    }

    private void DrawRow(StringBuilder tableBuilder, char borderChar, char paddingChar, Row row)
    {
      var rowBuilder = new StringBuilder();

      if(row != null)
        for(var lineIndex = 0; lineIndex < row.MaxHeight; ++lineIndex)
          DrawRowLine(rowBuilder, borderChar, paddingChar, row, lineIndex);
      else
        DrawRowLine(rowBuilder, borderChar, paddingChar, null, 0);

      tableBuilder.Append(rowBuilder);
    }

    private void DrawRowLine(StringBuilder rowBuilder, char borderChar, char paddingChar, Row row, int lineIndex)
    {
      var padding = new String(paddingChar, _cellColumnPadding);
      var innerPadding = new String(paddingChar, _innerCellColumnPadding);

      if(_drawCellBorders) rowBuilder.Append(borderChar);
      var firstColumn = true;
      rowBuilder.Append(padding);
      foreach(var column in _columns) {
        if(!firstColumn) {
          rowBuilder.Append(padding).Append(innerPadding);
          if(_drawCellBorders)
            rowBuilder.Append(borderChar).Append(padding).Append(innerPadding);
        }

        if(row != null) {
          var cell = row.GetCell(column.Name);
          var hAlign = GetCellHAlignment(cell, row, column);
          var vAlign = GetCellVAlignment(cell, row, column);

          var lines = cell.Lines;
          var adjLineIndex = vAlign == VAlign.Top ? lineIndex : lineIndex + lines.Length - row.MaxHeight;

          var foo = hAlign == HAlign.Left ? "-" : "";
          var template = $"{{0,{foo}{column.MaxWidth}}}";
          //var template = "{{0,{0}{1}}}"._(hAlign == HAlign.Left ? "-" : "", column.MaxWidth);
          rowBuilder.Append(template._(adjLineIndex < 0 || adjLineIndex >= lines.Length ? "" : lines[adjLineIndex]));
        }
        else
          rowBuilder.Append(new String(paddingChar, column.MaxWidth));

        firstColumn = false;
      }

      rowBuilder.Append(padding);
      if(_drawCellBorders) rowBuilder.Append(borderChar);

      rowBuilder.Append(Environment.NewLine);
    }

    private static HAlign GetCellHAlignment(Cell cell, Row row, Column column)
    {
      var align = cell.HAlign;
      if(align == HAlign.Default) align = row.HAlign;
      if(align == HAlign.Default) align = column.HAlign;
      if(align == HAlign.Default) align = HAlign.Left;
      return align;
    }

    private static VAlign GetCellVAlignment(Cell cell, Row row, Column column)
    {
      var align = cell.VAlign;
      if(align == VAlign.Default) align = row.VAlign;
      if(align == VAlign.Default) align = column.VAlign;
      if(align == VAlign.Default) align = VAlign.Top;
      return align;
    }
  }
}