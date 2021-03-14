using Newtonsoft.Json;
using System;


public class RTree<T> where T : Geometry {
    [JsonProperty]
    private Node<T> rootNode {get;set;}
    [JsonProperty]
    public readonly int maxPointsPerNode;

    public Node<T> Find(T geometry, bool insert = false) {
        
        Node<T> currentNode = null;
        var foundNode = this.rootNode.NextNode(geometry);
        while(foundNode != null) {
            currentNode = foundNode;
            if (insert) currentNode.enlargen(geometry);
            foundNode = foundNode.NextNode(geometry);
        }
        
        return currentNode;
    }

    private bool recursive = false;

    public void Insert(T geometry) {
       var foundNode = this.Find(geometry, true);

      
       if (foundNode.containsGeometry(geometry)) {
           return;
       }

       if (foundNode.numberOfGeometries() >= this.maxPointsPerNode) {
            if (this.recursive)
            {
                throw new Exception("Entering infinite loop, exiting");
            }
            foundNode.splitNode();
            this.recursive = true;
           this.Insert(geometry);
           
       } else {
           foundNode.addGeometry(geometry);
       }
        this.recursive = false;
    }

    public RTree() {
        this.rootNode = new Node<T>(0);

        this.rootNode.addChildNode(new Node<T>(1));
        this.rootNode.addChildNode(new Node<T>(1));

        this.maxPointsPerNode = 100;
    }

}