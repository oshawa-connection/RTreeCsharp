using System;

public class BBox {
    public float minX {get;set;}
    public float minY {get;set;}
    public float maxX {get;set;}
    public float maxY {get;set;}

    // What if all values are 0?
    public void enlargen(Geometry geometry) {
        var otherBBox = geometry.calculateBoundingBox();
        
        if (otherBBox.minX < this.minX) {
            this.minX = otherBBox.minX;
        }

        if (otherBBox.maxX > this.maxX) {
            this.maxX = otherBBox.maxX;
        }

        if (otherBBox.maxY > this.maxY) {
            this.maxY = otherBBox.maxY;
        }

        if (otherBBox.minY < this.minY) {
            this.minY = otherBBox.minY;
        }
    }

    public bool fullyContains(Geometry geometry) {
        var bbox = geometry.calculateBoundingBox();
        return this.fullyContains(bbox);
    }

    public bool fullyContains(BBox otherBbox) {
        if (this.minX > otherBbox.minX) return false;
        if (this.minY > otherBbox.minY) return false;
        if (this.maxX < otherBbox.maxX) return false;
        if (this.maxY < otherBbox.maxY) return false;
        
        return true;
    }

    public float calculateArea() {
        return (maxX - minX) * (maxY - minY);
    }
    
    ///<summary> 
    ///calculates the change in area needed to cover a new geometry
    ///</summary>
    public float calculateEnlargenedArea(Geometry geometry) {
        var otherBBox = geometry.calculateBoundingBox();
        var currentArea = this.calculateArea();
        //TODO: call enlargen on otherBBox.
        if (otherBBox.minX > this.minX) {
            otherBBox.minX = this.minX;
        }

        if (otherBBox.maxX < this.maxX) {
            otherBBox.maxX = this.maxX;
        }

        if (otherBBox.maxY < this.maxY) {
            otherBBox.maxY = this.maxY;
        }

        if (otherBBox.minY > this.minY) {
            otherBBox.minY = this.minY;
        }

        

        return otherBBox.calculateArea() - currentArea;
    }

    public (BBox,BBox) split(float splitX, float splitY) {
        var leftbbox = new BBox();
        var rightbbox = new BBox();

        leftbbox.minX = minX;
        leftbbox.maxX = splitX;
        leftbbox.maxY = splitY;
        leftbbox.minY = minY;

        rightbbox.minX = splitX;
        rightbbox.maxX = maxX;
        rightbbox.maxY = maxY;
        rightbbox.minY = splitY;

        return (leftbbox,rightbbox);
    }
}