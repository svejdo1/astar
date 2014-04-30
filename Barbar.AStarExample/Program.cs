using System;
using Barbar.Framework.Algorithms.Pathfinding;
using Barbar.Framework.Drawing;

namespace Barbar.AStarExample {
  class Program {
    static void Main(string[] args) {
      // empty map
      SimpleExample();

      // map with two walls in the bottom
      BlocksExample();

      // map with movement penalties set
      DifficultyExample();
    }

    private static void WriteLegend() {
      Console.Out.WriteLine("Legend:");
      Console.Out.WriteLine("o   path tile");
      Console.Out.WriteLine("V   visited tile (tile considered by algorithm)");
      Console.Out.WriteLine("B   blocked tile");
      Console.Out.WriteLine("0-9 path difficulty (9 is slowest, 0 is fastest)");
      Console.Out.WriteLine();
    }

    private static void SimpleExample() {
      Console.Clear();
      Console.Out.WriteLine("Simple example - empty map.");
      WriteLegend();

      var map = new MapSample();
      Console.Out.Write(map.ToString());
      var pathFinder = new AStarPathfinder(map, 1000, false, new ManhattanDistance());
      var from = new PointInt32(0, 0);
      var to = new PointInt32(map.Width - 1, map.Height - 1);
      Console.Out.WriteLine("Path from {0} to {1}.", from, to);
      var path = pathFinder.FindPath(null, from, to);
      Console.Out.Write(map.ToString(path, true));
      Console.Out.WriteLine("Press any key to continue ...");
      Console.In.ReadLine();
    }

    private static void BlocksExample() {
      Console.Clear();
      Console.Out.WriteLine("Example with blocks in map.");
      WriteLegend();

      var map = new MapSample();
      for (var y = map.Height / 2; y < map.Height; y++) {
        map[map.Width / 3, y].IsBlocker = true;
        map[2 * map.Width / 3, y].IsBlocker = true;
      }
      Console.Out.Write(map.ToString());
      var pathFinder = new AStarPathfinder(map, 1000, false, new ManhattanDistance());
      var from = new PointInt32(0, 0);
      var to = new PointInt32(map.Width - 1, map.Height - 1);
      Console.Out.WriteLine("Path from {0} to {1}.", from, to);
      var path = pathFinder.FindPath(null, from, to);
      Console.Out.Write(map.ToString(path, true));
      Console.Out.WriteLine("Press any key to continue ...");
      Console.In.ReadLine();
    }

    private static void DifficultyExample() {
      Console.Clear();
      Console.Out.WriteLine("Example with movement penalties in map.");
      WriteLegend();

      var map = new MapSample();
      for (var y = 0; y < map.Height / 3; y++) {
        map[map.Width / 3, y].Difficulty = 9;
        map[map.Width / 3, map.Height - 1 - y].Difficulty = 9;
        map[2 * map.Width / 3, y].Difficulty = 3;
        map[2 * map.Width / 3, map.Height - 1 - y].Difficulty = 3;
      }
      Console.Out.Write(map.ToString());
      var pathFinder = new AStarPathfinder(map, 1000, false, new ManhattanDistance());
      var from = new PointInt32(0, 0);
      var to = new PointInt32(map.Width - 1, map.Height - 1);
      Console.Out.WriteLine("Path from {0} to {1}.", from, to);
      var path = pathFinder.FindPath(null, from, to);
      Console.Out.Write(map.ToString(path, true));
      Console.Out.WriteLine("Press any key to continue ...");
      Console.In.ReadLine();
    }
  }
}