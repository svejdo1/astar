using System.Collections.Generic;
using Barbar.Framework.Drawing;

namespace Barbar.Framework.Algorithms.Pathfinding {
  public interface IPathfinder {
    IList<PointInt32> FindPath(IMover mover, PointInt32 start, PointInt32 target);
  }
}
