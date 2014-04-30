namespace Barbar.Framework.Drawing {
  public interface IPointInt32 : IReadOnlyPointInt32 {
    new int X { get; set; }
    new int Y { get; set; }
  }
}
