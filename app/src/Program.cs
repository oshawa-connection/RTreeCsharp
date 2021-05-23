using System;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HELLO WORLD");
            
            //SpeedTest(100,true);
            //SpeedTest(10000);
            //SpeedTest(20000);
        }


        

        static void SpeedTest(int maxNodes,bool dump = false)
        {
            
            var rTree = new RTree<Point>();

            var rand = new Random();

            var points = new Point[maxNodes];

            float y = 10.0f;
            for (var x = 0; x < maxNodes; x++)
            {
                y = rand.Next(0, 10);
                var newPoint = new Point(x, y);
                rTree.Insert(newPoint);
                points[x] = newPoint;
            }


            Stopwatch stopwatch = Stopwatch.StartNew();

            for(var searchId = 0; searchId < points.Length; searchId ++)
            {
                var x = rTree.FindBestNode(points[searchId]);
                if (x.depth > 100000000)
                {
                    throw new Exception("here to prevent optimisation");
                }
            }

            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds == 0)
            {
                Console.WriteLine("Elapsed milliseconds was too small, increase maxNodes!");
                
                
            } else
            {
                float searchesPerSecond = ((float)maxNodes) / (stopwatch.ElapsedMilliseconds * 1000);
                Console.WriteLine($"RTree searches/ second for {maxNodes} elements was : {searchesPerSecond}");
                Console.WriteLine($"RTree total time for {maxNodes} elements was : {stopwatch.ElapsedMilliseconds * 1000}");
            }

            Stopwatch stopwatch2 = Stopwatch.StartNew();

            for(var searchId = 0;searchId < points.Length; searchId ++)
            {
                Point currentPoint = points[searchId];
                for(var elementCount = 0;elementCount < points.Length;elementCount++)
                {
                    if (currentPoint == points[elementCount])
                    {
                        if (currentPoint.x != points[elementCount].x)
                        {
                            throw new Exception("here to prevent optimisation");
                        }
                        break;
                    }
                }
            }




            stopwatch2.Stop();

            float searchesPerSecondLinear = ((float)maxNodes) / (stopwatch2.ElapsedMilliseconds * 1000);
            Console.WriteLine($"Linear searches/ second for {maxNodes} elements was : {searchesPerSecondLinear}");
            Console.WriteLine($"Linear total time for {maxNodes} elements was : {stopwatch2.ElapsedMilliseconds * 1000}");

            if (dump)
            {
                string jsonString = JsonConvert.SerializeObject(rTree, Formatting.Indented);

                var tempFile = Path.GetTempFileName();

                File.WriteAllText(tempFile, jsonString);
                Console.WriteLine($"Wrote to {tempFile}");
            }
            

        }


    }
}
