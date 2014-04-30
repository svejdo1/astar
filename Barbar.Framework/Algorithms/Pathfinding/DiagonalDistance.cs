using Barbar.Framework.Drawing;

namespace Barbar.Framework.Algorithms.Pathfinding {
  // diagonal movement - assumes diag dist is 1, same as cardinals
  public class DiagonalDistance : IAStarHeuristic {
    public float GetCost(ITileMap map, IMover mover, PointInt32 point, PointInt32 start, PointInt32 target) {
      return System.Math.Max(System.Math.Abs(point.X - target.X), System.Math.Abs(point.Y - target.Y));
    }
  }
}
