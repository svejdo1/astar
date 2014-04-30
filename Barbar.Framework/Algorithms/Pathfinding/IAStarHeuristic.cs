using Barbar.Framework.Drawing;

namespace Barbar.Framework.Algorithms.Pathfinding {
  public interface IAStarHeuristic {
    float GetCost(ITileMap map, IMover mover, PointInt32 point, PointInt32 start, PointInt32 target);
  }
}
