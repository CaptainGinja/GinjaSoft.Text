namespace GinjaSoft.Text
{
  public partial class StringTableBuilder
  {
    public class Column
    {
      //
      // Private immutable data
      //
      private readonly string _name;

      //
      // Private mutable data
      //
      private int _maxWidth;
      private HAlign _hAlign;
      private VAlign _vAlign;


      //
      // Public constructor
      //
      public Column(string name)
      {
        _name = name;
        _hAlign = HAlign.Default;
        _vAlign = VAlign.Default;
        _maxWidth = 0;
      }

      //
      // Public (read only) properties
      //
      public string Name => _name;
      public HAlign HAlign => _hAlign;
      public VAlign VAlign => _vAlign;

      //
      // Public methods
      //
      public Column SetHAlignLeft() { return SetHAlign(HAlign.Left); }
      public Column SetHAlignRight() { return SetHAlign(HAlign.Right); }
      public Column SetVAlignTop() { return SetVAlign(VAlign.Top); }
      public Column SetVAlignBottom() { return SetVAlign(VAlign.Bottom); }


      //
      // Internal (read/write) properties
      //
      internal int MaxWidth { get => _maxWidth; set => _maxWidth = value; }


      //
      // Private methods
      //
      private Column SetHAlign(HAlign hAlign) { _hAlign = hAlign; return this; }
      private Column SetVAlign(VAlign vAlign) { _vAlign = vAlign; return this; }
    }
  }
}
