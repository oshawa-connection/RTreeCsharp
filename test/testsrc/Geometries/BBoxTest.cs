using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class BBoxTest
    {
        [TestMethod]
        public void TestCalulatesArea()
        {
            var bbox = new BBox();
            bbox.minX = 1.0f;
            bbox.minY = 1.0f;

            bbox.maxX = 3.0f;
            bbox.maxY = 3.0f;

            Assert.AreEqual(bbox.calculateArea(),4.0f);
            
        }

        [TestMethod]
        public void TestCalulatesEnlargenedArea()
        {
            var bbox = new BBox();
            bbox.minX = 1.0f;
            bbox.minY = 1.0f;

            bbox.maxX = 3.0f;
            bbox.maxY = 3.0f;

            var newPoint = new Point(4.0f,4.0f);

            float newArea = bbox.calculateEnlargenedArea(newPoint);

            Assert.AreEqual(newArea,9.0f - 4.0f);
            
        }


        [TestMethod]
        public void TestCanSplit()
        {
            var bbox = new BBox();
            bbox.minX = 1.0f;
            bbox.minY = 1.0f;

            bbox.maxX = 3.0f;
            bbox.maxY = 3.0f;

            var newPoint = new Point(4.0f,4.0f);

            (BBox left, BBox right) = bbox.split();

            Assert.AreEqual(bbox.minX,left.minX);

            Assert.AreEqual(left.maxX, (3.0f- 1.0f)/ 2.0f);
            
        }
    }
}
