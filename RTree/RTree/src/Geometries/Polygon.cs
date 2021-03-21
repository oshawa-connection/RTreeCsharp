using System;
using System.Collections.Generic;
using System.Linq;
public class Polygon : Geometry {
    private readonly Point[] vertices;

    public override BBox calculateBoundingBox() {
        var bbox = new BBox();
        bbox.minX = vertices.Min(p =>p.x);
        bbox.minY = vertices.Min(p =>p.y);
        bbox.maxX = vertices.Max(p =>p.x);
        bbox.maxY = vertices.Max(p =>p.y);
        return bbox;
    }

    public bool fullyContains(Geometry geometry) {

        var Bbox = this.calculateBoundingBox();
        return Bbox.fullyContains(geometry.calculateBoundingBox());
        
        
        
    }
    public Polygon(Point[] vertices) {
        // ensure closed
        this.vertices = vertices;
    }

    public Polygon()
    {

    }
}