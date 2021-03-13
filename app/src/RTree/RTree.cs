using System;


public class RTree<T> where T : Geometry {
    private Node<T> rootNode {get;set;}
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

    public void Insert(T geometry) {
       var foundNode = this.Find(geometry, true);
       if (foundNode is not null)
       {

       }
       

       if (foundNode.containsGeometry(geometry)) {
           return;
       }

       if (foundNode.numberOfGeometries() >= this.maxPointsPerNode) {
           foundNode.splitNode();
           this.Insert(geometry);
           
       } else {
           foundNode.addGeometry(geometry);
       }
    }

    public RTree() {
        this.rootNode = new Node<T>();

        this.rootNode.addChildNode(new Node<T>());
        this.rootNode.addChildNode(new Node<T>());

        this.maxPointsPerNode = 5;
    }

}