using System.Collections.Generic;
using Barbar.Framework.Drawing;

namespace Barbar.Framework.Algorithms.Pathfinding {
  internal class AStarMatrix {
    private int m_Width;
    private Dictionary<int, AStarNode> m_Nodes;
    
    public AStarMatrix(int width) {
      m_Width = width;
      m_Nodes = new Dictionary<int, AStarNode>();
    }

    public void Clear() {
      m_Nodes.Clear();
    }
    
    public AStarNode this[PointInt32 point] {
      get {
        int index = point.Y * m_Width + point.X;
        AStarNode result;
        if (!m_Nodes.TryGetValue(index, out result)) {
          result = new AStarNode(point);
          m_Nodes[index] = result;
        }
        
        return result;
      }
      set {
        int index = point.Y * m_Width + point.X;
        m_Nodes[index] = value;
      }
    }
  }
}
