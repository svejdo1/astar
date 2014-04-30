
using Barbar.Framework.Drawing;
namespace Barbar.Framework.Algorithms.Pathfinding {
  // diagonals are considered a little farther than cardinal directions
  public class EuclideanDistance : IAStarHeuristic {
    private float m_Coefficient;

    public EuclideanDistance()
      : this(1f) {
    }

    public EuclideanDistance(float coefficient) {
      m_Coefficient = coefficient;
    }

    public float GetCost(ITileMap map, IMover mover, PointInt32 point, PointInt32 start, PointInt32 target) {
      float dx = target.X - point.X;
      float dy = target.Y - point.Y;
      return (float)System.Math.Sqrt((double)((dx * dx) + (dy * dy))) * m_Coefficient;
    }
  }
}
