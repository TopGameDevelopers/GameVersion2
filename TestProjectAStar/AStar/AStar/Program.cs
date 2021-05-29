using System;
using System.Drawing;
using System.Numerics;

namespace AStar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var target = new Vector2(-15.78f, 7.5f);
            var monster = new Vector2(-10.45f, 0);
            var gridSize = 100;
            var obstacles = new Point[]
            {
                new Point(3, 7),
                new Point(-5, 5),
                new Point(-15, 75),
                new Point(54, 67),
                new Point(7, 7),
                new Point(0, 88),
                new Point(11, 13),
                new Point(6,4),
                new Point(-6,-4)
            };
            var searcher = new Searcher(target, monster, gridSize, obstacles);
            var path = searcher.GetPathAStar();
            foreach (var point in path)
            {
                Console.WriteLine(point);
            }
        }
    }
}