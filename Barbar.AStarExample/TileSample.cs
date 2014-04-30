using System;
using System.Globalization;

namespace Barbar.AStarExample {
  public class TileSample {
    private int m_Difficulty;

    public int Difficulty {
      get { return m_Difficulty; }
      set { m_Difficulty = value; }
    }

    public bool IsBlocker { get; set; }
    public bool IsVisited { get; set; }

    public override string ToString() {
      return ToString(true);
    }

    public string ToString(bool showVisited) {
      if (showVisited && IsVisited) {
        return "V";
      }

      if (IsBlocker) {
        return "B";
      }

      return Convert.ToString(m_Difficulty, CultureInfo.InvariantCulture);
    }
  }
}
