using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace Barbar.Framework.Drawing {
  [DataContract]
  public struct PointInt32 : IPointInt32 {
    private int m_X;
    private int m_Y;
    
    [DataMember]
    public int X {
      get { return m_X; }
      set { m_X = value; }
    }

    [DataMember]
    public int Y {
      get { return m_Y; }
      set { m_Y = value; }
    }

    public PointInt32(int x, int y) {
      m_X = x;
      m_Y = y;
    }

    public override int GetHashCode() {
      return m_X * 1024 + m_Y;
    }

    public override bool Equals(object obj) {
      var another = (IReadOnlyPointInt32)obj;
      return another.X == X && another.Y == Y;
    }

    public override string ToString() {
      return string.Format(CultureInfo.InvariantCulture, "[{0};{1}]", X, Y);
    }

    public bool IsNeighbor(IReadOnlyPointInt32 anotherPoint) {
      int dx = Math.Abs(X - anotherPoint.X);
      int dy = Math.Abs(Y - anotherPoint.Y);
      return dx <= 1 && dy <= 1;
    }

    public bool IsOrthogonalNeighbor(IReadOnlyPointInt32 anotherPoint) {
      return (X == anotherPoint.X && (Y == anotherPoint.Y - 1 || Y == anotherPoint.Y + 1)) ||
        (Y == anotherPoint.Y && (X == anotherPoint.X - 1 || X == anotherPoint.X + 1));
    }

    public bool IsOrthogonalNeighborOrEqual(IReadOnlyPointInt32 anotherPoint) {
      return (X == anotherPoint.X && Y == anotherPoint.Y) ||
        IsOrthogonalNeighbor(anotherPoint);
    }

    public double GetAngleInDegrees(IPointInt32 stop) {
      var dx = stop.X - X;
      var dy = stop.Y - Y;

      double angle = Math.Atan2(-dx, dy);
      var degrees = 90 + ((angle * 180) / Math.PI);
      if (degrees < 0) {
        degrees += 360;
      }
      return degrees;
    }

    public double Distance(IReadOnlyPointInt32 stop) {
      int dx = stop.X - X;
      int dy = stop.Y - Y;
      return Math.Sqrt(dx * dx + dy * dy);
    }

    public static bool IsOrthogonal(IList<IReadOnlyPointInt32> coordinates) {
      if (coordinates == null || coordinates.Count < 2) {
        return true;
      }

      var first = coordinates[0];

      bool dx = false;
      bool dy = false;

      for (var i = 1; i < coordinates.Count; i++) {
        dx |= (first.X != coordinates[i].X);
        dy |= (first.Y != coordinates[i].Y);
      }

      return !(dx && dy);
    }

    public static IEnumerable<PointInt32> GetAdjectedCoordinates(IReadOnlyPointInt32 coordinates) {
      yield return new PointInt32(coordinates.X - 1, coordinates.Y);
      yield return new PointInt32(coordinates.X + 1, coordinates.Y);
      yield return new PointInt32(coordinates.X - 1, coordinates.Y - 1);
      yield return new PointInt32(coordinates.X, coordinates.Y - 1);
      yield return new PointInt32(coordinates.X + 1, coordinates.Y - 1);
      yield return new PointInt32(coordinates.X - 1, coordinates.Y + 1);
      yield return new PointInt32(coordinates.X, coordinates.Y + 1);
      yield return new PointInt32(coordinates.X + 1, coordinates.Y + 1);
    }


    public static IEnumerable<PointInt32> GetOrthogonalAdjectedCoordinates(IReadOnlyPointInt32 coordinates, int step) {
      yield return new PointInt32(coordinates.X - step, coordinates.Y);
      yield return new PointInt32(coordinates.X, coordinates.Y - step);
      yield return new PointInt32(coordinates.X + step, coordinates.Y);
      yield return new PointInt32(coordinates.X, coordinates.Y + step);
    }

    public static IEnumerable<PointInt32> GetOrthogonalAdjectedCoordinates(IReadOnlyPointInt32 coordinates) {
      return GetOrthogonalAdjectedCoordinates(coordinates, 1);
    }

    public PointInt32(IReadOnlyPointInt32 point) {
      m_X = point.X;
      m_Y = point.Y;
    }
  }
}
