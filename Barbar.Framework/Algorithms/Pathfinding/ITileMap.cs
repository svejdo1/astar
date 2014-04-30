using Barbar.Framework.Drawing;

namespace Barbar.Framework.Algorithms.Pathfinding {
  public interface ITileMap {
    int Height { get; }
    int Width { get; }

    float GetCost(IMover mover, PointInt32 source, PointInt32 target);
    bool IsBlocked(IMover mover, PointInt32 point);
    void PathfinderCallback(PointInt32 point);
  }
}
