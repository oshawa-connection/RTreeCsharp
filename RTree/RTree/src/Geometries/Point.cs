using System;


public class Point :Geometry {
    public readonly float x;
    public readonly float y;

    public override BBox calculateBoundingBox() {
        var bbox = new BBox();
        bbox.maxX = this.x;
        bbox.minX = this.x;
        bbox.maxY =this.y;
        bbox.minY =this.y;
        return bbox;
    }

    public virtual bool Equals(Point other)
    {
        Console.WriteLine("Using ovveridden Point comparison!");
        return other.x == this.x && other.y == this.y;
    }

    public Point(float x, float y) {
        this.x = x;
        this.y = y;
    }

    public Point()
    {
        
    }
}