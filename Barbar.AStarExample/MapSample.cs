using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Barbar.Framework.Algorithms.Pathfinding;
using Barbar.Framework.Drawing;

namespace Barbar.AStarExample {
  public class MapSample : ITileMap {
    private TileSample[][] m_Map;
    private const int WIDTH = 40;
    private const int HEIGHT = 15;
    
    public MapSample() {
      m_Map = new TileSample[WIDTH][];
      for (var x = 0; x < m_Map.Length; x++) {
        m_Map[x] = new TileSample[HEIGHT];
        for (var y = 0; y < m_Map[x].Length; y++) {
          m_Map[x][y] = new TileSample();
        }
      }
    }

    public TileSample this[int x, int y] {
      get {
        return m_Map[x][y];
      }
    }

    public override string ToString() {
      return ToString(null, true);
    }

    public string ToString(IList<PointInt32> path, bool showVisited) {
      var builder = new StringBuilder(WIDTH * HEIGHT);
      // header
      builder.Append("X|");
      for (var x = 0; x < Width; x++) {
        builder.Append(x % 10);
      }
      builder.AppendLine();
      builder.Append("-+".PadRight(WIDTH + 2, '-'));
      builder.AppendLine();
      
      for (var y = 0; y < Height; y++) {
        builder.Append(y % 10);
        builder.Append("|");
        for (var x = 0; x < Width; x++) {
          if (path != null && path.Any(p => p.X == x && p.Y == y)) {
            builder.Append("o");
          } else {
            builder.Append(m_Map[x][y].ToString(showVisited));
          }
        }
        builder.AppendLine();
      }
      return builder.ToString();
    }


    public int Height {
      get { return HEIGHT; }
    }

    public int Width {
      get { return WIDTH; }
    }

    public float GetCost(IMover mover, PointInt32 source, PointInt32 target) {
      return m_Map[target.X][target.Y].Difficulty;
    }

    public bool IsBlocked(IMover mover, PointInt32 point) {
      return m_Map[point.X][point.Y].IsBlocker;
    }

    public void PathfinderCallback(PointInt32 point) {
      m_Map[point.X][point.Y].IsVisited = true;
    }
  }
}
