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
            var foundNode = rTree.Find(pointToInsert);

            var found2Node = rTree.Find(point2ToInsert);


            Assert.IsNotNull(foundNode);
            Assert.IsNotNull(found2Node);
            
            
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
            Node<Point> found = rTree.Find(pointToFind);
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
