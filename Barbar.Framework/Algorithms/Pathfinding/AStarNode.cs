using System;
using Barbar.Framework.Drawing;

namespace Barbar.Framework.Algorithms.Pathfinding {
  internal class AStarNode : IComparable {
    public float Cost { get; set; }
    public int Depth { get; set; }
    public float Heuristic { get; set; }
    public AStarNode Parent { get; set; }
    public PointInt32 Point { get; set; }

    public AStarNode(PointInt32 point) {
      this.Point = point;
    }

    public int CompareTo(object other) {
      var o = (AStarNode)other;
      float f = this.Heuristic + this.Cost;
      float of = o.Heuristic + o.Cost;
      if (f < of) {
        return -1;
      }
      if (f > of) {
        return 1;
      }
      return 0;
    }

    public int SetParent(AStarNode parent) {
      this.Depth = parent.Depth + 1;
      this.Parent = parent;
      return this.Depth;
    }
  }
}
