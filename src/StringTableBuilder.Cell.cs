namespace GinjaSoft.Text
{
  public partial class StringTableBuilder
  {
    public class Cell
    {
      //
      // Private immutable data
      //
      private readonly string[] _lines;


      // TODO:  Can I make these immutable?  I really want these to be initialized as part of the ctor.

      //
      // Private mutable data
      //
      private HAlign _hAlign;
      private VAlign _vAlign;


      //
      // Public constructor
      //
      public Cell(string[] lines)
      {
        _lines = lines;
        _hAlign = HAlign.Default;
        _vAlign = VAlign.Default;
      }


      //
      // Public (read only) properties
      //
      public string Value => string.Join("\n", Lines);
      public string[] Lines => _lines;
      public HAlign HAlign => _hAlign;
      public VAlign VAlign => _vAlign;


      // TODO:  Rempve these?  They are sortof redundant.  We can just call the methods with args instead.

      //
      // Public methods
      //
      public Cell SetHAlignLeft() { return SetHAlign(HAlign.Left); }
      public Cell SetHAlignRight() { return SetHAlign(HAlign.Right); }
      public Cell SetVAlignTop() { return SetVAlign(VAlign.Top); }
      public Cell SetVAlignBottom() { return SetVAlign(VAlign.Bottom); }


      // TODO: Have these be initializations as part of the ctor?

      //
      // Private methods
      //
      private Cell SetHAlign(HAlign hAlign) { _hAlign = hAlign; return this; }
      private Cell SetVAlign(VAlign vAlign) { _vAlign = vAlign; return this; }
    }
  }
}
