using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AStar;
using NUnit.Framework;

namespace TestAStar
{
    public class TestsWhenNoObstacles
    {
        private int _gridSize;
        private Point[] _obstacles;
        [SetUp]
        public void Setup()
        {
            _gridSize = 100;
            _obstacles = System.Array.Empty<Point>();
        }

        [Test]
        public void TestIfSamePosition()
        {
            var target = new Vector2(1, 0);
            var monster = new Vector2(1, 0);
            var searcher = new Searcher(target, monster, _gridSize, _obstacles);
            var path = searcher.GetPathAStar();
            Assert.AreEqual(null, path);
        }

        [Test]
        public void TestIfIntCoordinates()
        {
            var target = new Vector2(5, 5);
            var monster = new Vector2(10, 0);
            var searcher = new Searcher(target, monster, _gridSize, _obstacles);
            var path = searcher.GetPathAStar();
            var expected = new List<Point>
            {
                new Point(10, 0),
                new Point(9, 1),
                new Point(8, 2),
                new Point(7, 3),
                new Point(6, 4),
                new Point(5, 5)
            };
            Assert.AreEqual(expected, path);
        }
        
        [Test]
        public void TestIfIntNegativeCoordinates()
        {
            var target = new Vector2(-5, -5);
            var monster = new Vector2(-10, 0);
            var searcher = new Searcher(target, monster, _gridSize, _obstacles);
            var path = searcher.GetPathAStar();
            var expected = new List<Point>
            {
                new Point(-10, 0),
                new Point(-9, -1),
                new Point(-8, -2),
                new Point(-7, -3),
                new Point(-6, -4),
                new Point(-5, -5)
            };
            Assert.AreEqual(expected, path);
        }
        
        [Test]
        public void TestIfNotInGrid()
        {
            var target = new Vector2(5, 5);
            var monster = new Vector2(1000, 0);
            var searcher = new Searcher(target, monster, _gridSize, _obstacles);
            var path = searcher.GetPathAStar();
            Assert.AreEqual(null, path);
        }
        
        [Test]
        public void TestIfFloatCoordinates()
        {
            var target = new Vector2(5.78f, 5.16f);
            var monster = new Vector2(10.78f, 0);
            var searcher = new Searcher(target, monster, _gridSize, _obstacles);
            var path = searcher.GetPathAStar();
            var expected = new List<Point>
            {
                new Point(10, 0),
                new Point(9, 1),
                new Point(8, 2),
                new Point(7, 3),
                new Point(6, 4),
                new Point(5, 5)
            };
            Assert.AreEqual(expected, path);
        }
        
        [Test]
        public void TestIfFloatNegativeCoordinates()
        {
            var target = new Vector2(-5.78f, -5.16f);
            var monster = new Vector2(-10.78f, 0);
            var searcher = new Searcher(target, monster, _gridSize, _obstacles);
            var path = searcher.GetPathAStar();
            var expected = new List<Point>
            {
                new Point(-10, 0),
                new Point(-9, -1),
                new Point(-8, -2),
                new Point(-7, -3),
                new Point(-6, -4),
                new Point(-5, -5)
            };
            Assert.AreEqual(expected, path);
        }
    }

    public class TestWithObstacles
    {
        private int _gridSize;
        private Point[] _obstacles;

        [SetUp]
        public void Setup()
        {
            _gridSize = 100;
            _obstacles = new Point[]
            {
                new Point(3, 7),
                new Point(-5, 5),
                new Point(-15, 75),
                new Point(54, 67),
                new Point(7, 7),
                new Point(0, 88),
                new Point(11, 13),
                new Point(6, 4),
                new Point(-6,-4)
            };
        }
        
        [Test]
        public void TestIfIntCoordinates()
        {
            var target = new Vector2(5, 5);
            var monster = new Vector2(10, 0);
            var searcher = new Searcher(target, monster, _gridSize, _obstacles);
            var path = searcher.GetPathAStar();
            var expected = new List<Point>
            {
                new Point(10, 0),
                new Point(9, 1),
                new Point(8, 2),
                new Point(7, 3),
                new Point(7,4),
                new Point(6, 5),
                new Point(5, 5)
            };
            Assert.AreEqual(expected, path);
        }
        
        [Test]
        public void TestIfIntNegativeCoordinates()
        {
            var target = new Vector2(-5, -5);
            var monster = new Vector2(-10, 0);
            var searcher = new Searcher(target, monster, _gridSize, _obstacles);
            var path = searcher.GetPathAStar();
            var expected = new List<Point>
            {
                new Point(-10, 0),
                new Point(-9, -1),
                new Point(-8, -2),
                new Point(-7, -3),
                new Point(-6,-3),
                new Point(-5,-4),
                new Point(-5, -5)
            };
            Assert.AreEqual(expected, path);
        }
        
        [Test]
        public void TestIfFloatCoordinates()
        {
            var target = new Vector2(15.78f, 7.5f);
            var monster = new Vector2(10.45f, 0);
            var searcher = new Searcher(target, monster, _gridSize, _obstacles);
            var path = searcher.GetPathAStar();
            var expected = new List<Point>
            {
                new Point(10, 0),
                new Point(10, 1),
                new Point(11, 2),
                new Point(12, 3),
                new Point(13,4),
                new Point(14, 5),
                new Point(14, 6),
                new Point(15, 7)
            };
            Assert.AreEqual(expected, path);
        }
        
        [Test]
        public void TestIfFloatNegativeCoordinates()
        {
            var target = new Vector2(-15.78f, 7.5f);
            var monster = new Vector2(-10.45f, 0);
            var searcher = new Searcher(target, monster, _gridSize, _obstacles);
            var path = searcher.GetPathAStar();
            var expected = new List<Point>
            {
                new Point(-10, 0),
                new Point(-11, 1),
                new Point(-12, 2),
                new Point(-13,3),
                new Point(-14, 4),
                new Point(-15, 5),
                new Point(-15, 6),
                new Point(-15,7)
            };
            Assert.AreEqual(expected, path);
        }
    }
}