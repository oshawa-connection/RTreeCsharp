using System;
using System.Collections.Generic;
using System.Linq;

public class Node<T> where T : Geometry {
    public string id {get;set;}

    private BBox bbox {get;set;}

    private List<Node<T>> childNodes {get;set;}
    private List<T> geometries {get;set;}

    public bool isLeaf() {
        return this.childNodes.Count == 0;    
    }

    public Node<T> nextNode(T geometry) {
        if(this.isLeaf()) {
            return null;
        }
        
        var currentMin = float.PositiveInfinity;
        float nodeMin;
        
        Node<T> bestChildNode = null;

        foreach(var childNode in this.childNodes) {
            nodeMin = childNode.enlargenedArea(geometry);
            if (nodeMin < currentMin){
                currentMin = nodeMin;
                bestChildNode = childNode;
            }
        }

        if (bestChildNode == null) {
            throw new Exception("Best child node was null");
        }

        return bestChildNode;
    }

    public float enlargenedArea(T geometry) {
        return this.bbox.calculateEnlargenedArea(geometry);
    }

    public int numberOfGeometries() {
        return this.geometries.Count();
    }

    public bool containsGeometry(T geometry) {
        return this.geometries.Contains(geometry);
    }

    public void addGeometry(T geometry) {
        // Check will fit in bbox
        this.geometries.Add(geometry);
    }

    public void enlargen(T geometry) {
        this.bbox.enlargen(geometry);
    }

    public void addChildNode(Node<T> node){
        this.childNodes.Add(node);
    }

    public (Node<T>, Node<T>,Node<T>) splitNode() {
        var x = new Node<T>();
        var y = new Node<T>();
        return (x, y,x);
    }

    public Node(string id = null, BBox bbox = null) {
        if (id == null) {
            id = Guid.NewGuid().ToString();
        }
        this.id = id;
        this.childNodes = new List<Node<T>>();
        this.geometries = new List<T>();

        if (bbox == null) {
            this.bbox = new BBox();
        } else {
            this.bbox = bbox;
        }
        
    }


}