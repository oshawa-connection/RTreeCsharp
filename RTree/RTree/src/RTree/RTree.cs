using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;

public class RTree<T> where T : Geometry, new()
{
    [JsonProperty]
    private Node<T> rootNode {get;set;}
    [JsonProperty]
    public readonly int maxPointsPerNode;

    public Node<T> FindNodeContainingGeometry(T geometry) {
        Node<T> currentNode = null;
        var foundNode = this.rootNode.NextNode(geometry);
        while(foundNode != null) {
            currentNode = foundNode;
            foundNode = foundNode.NextNode(geometry);
        }
        
        if (currentNode.containsGeometry(geometry)) {
            return currentNode;
        } else {
            return null;
        }
    }

    ///TODO: Split this method somehow
    ///
    public Node<T> FindBestNode(T geometry, bool insert = false) {
        
        Node<T> currentNode = null;
        var foundNode = this.rootNode.NextNode(geometry);
        while(foundNode != null) {
            currentNode = foundNode;
            if (insert) currentNode.enlargen(geometry);
            foundNode = foundNode.NextNode(geometry);
        }
        
        return currentNode;

    }

    private void FindAllLeaves(T geometry, Node<T> currentNode, ref List<Node<T>> foundLeaves)
    {
        var foundNodes = currentNode.findChildrenThatIntersect(geometry);

        if (foundNodes == null)
        {
            return;
        }

        foundLeaves =foundLeaves.Concat(foundNodes.Where(d => d.isLeaf())).ToList();

        if (foundNodes.All(d => d.isLeaf()))
        {
            return;
        }


        var foundNonLeaves = foundNodes.Where(d => !d.isLeaf());

        foreach(var search in foundNonLeaves)
        {
            this.FindAllLeaves(geometry,search, ref foundLeaves);
        }

        
    }

    public T NearestNeighbour(T geometry)
    {
        var foundLeaves = new List<Node<T>>();
        FindAllLeaves(geometry, this.rootNode, ref foundLeaves);

        foreach(var leaf in foundLeaves) {
            //if (leaf.containsGeometry(geometry)
        }

        foundLeaves.ForEach(d => d.containsGeometry(geometry));

        Console.WriteLine($"Finished! Found ${foundLeaves.Count} leaves");


        // find any bbox that intersects this geometry
        return geometry;
    }


    private bool recursive = false;

    public void Insert(T geometry) {
       var foundNode = this.FindBestNode(geometry, true);

        // Don't allow duplicates
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

        this.maxPointsPerNode = 5;
    }

}