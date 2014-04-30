using System.Collections.Generic;
using Barbar.Framework.Collections;
using Barbar.Framework.Drawing;

namespace Barbar.Framework.Algorithms.Pathfinding {
  public class AStarPathfinder : IPathfinder {
    private bool m_AllowDiagMovement;
    private List<AStarNode> m_Closed;
    private IAStarHeuristic m_Heuristic;
    private ITileMap m_Map;
    private int m_MaxSearchDistance;
    private AStarMatrix m_Nodes;
    private OrderedList<AStarNode> m_Open;

    public AStarPathfinder(ITileMap map, int maxSearchDistance, bool allowDiagMovement, IAStarHeuristic heuristic) {
      m_Closed = new List<AStarNode>();
      m_Open = new OrderedList<AStarNode>();
      m_Heuristic = heuristic;
      m_Map = map;
      m_MaxSearchDistance = maxSearchDistance;
      m_AllowDiagMovement = allowDiagMovement;
      m_Nodes = new AStarMatrix(map.Width);
    }

    public IList<PointInt32> FindPath(IMover mover, PointInt32 start, PointInt32 target) {
      if (m_Map.IsBlocked(mover, target)) {
        return null;
      }
      m_Nodes.Clear();
      m_Nodes[start].Cost = 0f;
      m_Nodes[start].Depth = 0;
      m_Closed.Clear();
      m_Open.Clear();
      m_Open.Add(m_Nodes[start]);
      m_Nodes[target].Parent = null;
      int maxDepth = 0;
      while ((maxDepth < m_MaxSearchDistance) && (m_Open.Count != 0)) {
        var current = m_Open.First();
        if (current == m_Nodes[target]) {
          break;
        }
        m_Open.Remove(current);
        m_Closed.Add(current);
        for (int x = -1; x < 2; x++) {
          for (int y = -1; y < 2; y++) {
            if (((x != 0) || (y != 0)) && (m_AllowDiagMovement || ((x == 0) || (y == 0)))) {
              var point = new PointInt32(current.Point.X + x, current.Point.Y + y);
              if (IsValidLocation(mover, start, point)) {
                float nextStepCost = current.Cost + m_Map.GetCost(mover, current.Point, point);
                var neighbour = m_Nodes[point];
                m_Map.PathfinderCallback(point);
                if (nextStepCost < neighbour.Cost) {
                  if (m_Open.Contains(neighbour)) {
                    m_Open.Remove(neighbour);
                  }
                  if (m_Closed.Contains(neighbour)) {
                    m_Closed.Remove(neighbour);
                  }
                }
                if (!(m_Open.Contains(neighbour) || m_Closed.Contains(neighbour))) {
                  neighbour.Cost = nextStepCost;
                  neighbour.Heuristic = m_Heuristic.GetCost(m_Map, mover, point, start, target);
                  maxDepth = System.Math.Max(maxDepth, neighbour.SetParent(current));
                  m_Open.Add(neighbour);
                }
              }
            }
          }
        }
      }
      if (m_Nodes[target].Parent == null) {
        return null;
      }
      var path = new List<PointInt32>();
      for (var targetNode = m_Nodes[target]; targetNode != m_Nodes[start]; targetNode = targetNode.Parent) {
        path.Insert(0, targetNode.Point);
      }
      path.Insert(0, start);
      return path;
    }

    private bool IsValidLocation(IMover mover, PointInt32 start, PointInt32 point) {
      bool invalid = (((point.X < 0) || (point.Y < 0)) || (point.X >= m_Map.Width)) || (point.Y >= m_Map.Height);
      if (!(invalid || ((start.X == point.X) && (start.Y == point.Y)))) {
        invalid = m_Map.IsBlocked(mover, point);
      }
      return !invalid;
    }
  }
}
