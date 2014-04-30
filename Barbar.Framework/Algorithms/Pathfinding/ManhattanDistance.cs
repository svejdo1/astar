using Barbar.Framework.Drawing;

namespace Barbar.Framework.Algorithms.Pathfinding {
  // linear movement - no diagonals - just cardinal directions (NSEW)
  public class ManhattanDistance : IAStarHeuristic {
    private float m_Coefficient;

    public ManhattanDistance()
      : this(1f) {
    }
    
    public ManhattanDistance(float coefficient) {
      m_Coefficient = coefficient;
    }


    public float GetCost(ITileMap map, IMover mover, PointInt32 point, PointInt32 start, PointInt32 target) {
      return (System.Math.Abs(point.X - target.X) + System.Math.Abs(point.Y - target.Y)) * m_Coefficient;
    }
  }
}
