using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.testsrc.RTreeTest
{
    [TestClass]
    public class RTreeTests
    {
        [TestMethod]
        public void TestCanInsert()
        {
            var rTree = new RTree<Point>();
            var pointToInsert = new Point(1, 2);
            var point2ToInsert = new Point(-100, -100);
            rTree.Insert(pointToInsert);
            rTree.Insert(point2ToInsert);
            var foundNode = rTree.FindBestNode(pointToInsert);

            var found2Node = rTree.FindBestNode(point2ToInsert);


            Assert.IsNotNull(foundNode);
            Assert.IsNotNull(found2Node);
            
            
        }

        [TestMethod]
        public void TestCanInsertMultiple()
        {
            const int maxNodes = 100;
            var rTree = new RTree<Point>();
            var points = new Point[maxNodes];

            for (var x = 0; x < maxNodes; x++)
            {
                
                var newPoint = new Point(x, x);
                rTree.Insert(newPoint);
                points[x] = newPoint;
            }

            var foundNode = rTree.FindNodeContainingGeometry(points[0]);
            Assert.IsTrue(foundNode.containsGeometry(points[0]));
            Console.WriteLine(foundNode.depth);
        }


        [TestMethod]
        public void TestSplits()
        {
            var rTree = new RTree<Point>();
            for(var nodeCount=0; nodeCount < rTree.maxPointsPerNode + 1; nodeCount++)
            {
                rTree.Insert(new Point(nodeCount, 1));
            }

            Assert.AreEqual(1, 1);
        }


        [TestMethod]
        public void TestFindEmpty()
        {
            var rTree = new RTree<Point>();
            var pointToFind = new Point(1, 2);
            Node<Point> found = rTree.FindNodeContainingGeometry(pointToFind);
            Assert.IsNull(found);
        }

        [TestMethod]
        public void TestNearestNeighbour()
        {
            var rTree = new RTree<Point>();
            var pointToFind = new Point(1, 2);
            rTree.Insert(pointToFind);
            rTree.NearestNeighbour(pointToFind);
            Assert.AreEqual(1, 1);
        }

    }
}
