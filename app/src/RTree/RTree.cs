using System;


public class RTree<T> where T : Geometry {
    private Node<T> rootNode {get;set;}
    private readonly int maxPointsPerNode;
    public Node<T> find(T geometry, bool insert = false) {
        
        Node<T> currentNode = null;
        var foundNode = this.rootNode.nextNode(geometry);
        while(foundNode != null) {
            currentNode = foundNode;
            if (insert) currentNode.enlargen(geometry);
            foundNode = foundNode.nextNode(geometry);
        }
        
        return currentNode;
    }

    public void insert(T geometry) {
       var foundNode = this.find(geometry);
       if (foundNode.containsGeometry(geometry)) {
           return;
       }

       if (foundNode.numberOfGeometries() >= this.maxPointsPerNode) {
           (Node<T> x, Node<T> y,Node<T> choice) = foundNode.splitNode();
           choice.addGeometry(geometry);
       }

    
       
    }

    public RTree() {
        this.rootNode = new Node<T>();
        this.maxPointsPerNode = 5;
    }

}