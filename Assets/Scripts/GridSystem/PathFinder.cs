using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class PathFinder : MonoBehaviour
    {
        [SerializeField] private Vector2Int startCoordinates;
        public Vector2Int StartCoordinates{get { return startCoordinates;}}
    
        [SerializeField] private Vector2Int destinationCoordinates;
        public Vector2Int DestinationCoordinates{get { return destinationCoordinates;}}


        private Node startNode;
        private Node destinationNode;
        Node currentSearchNode;

        private Queue<Node> frontier = new Queue<Node>();
        private Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    
        private Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down,};
        private GridManager gridManager;
        private Grid grid;


        private void Awake()
        {
            gridManager = FindObjectOfType<GridManager>();
            
            if (gridManager != null)
            {
                grid = gridManager.Grid;
                startNode = grid._gridArray[startCoordinates.x,startCoordinates.y]; 
                destinationNode = grid._gridArray[destinationCoordinates.x,destinationCoordinates.y];
            }
        }

        private void Start()
        {
            GetNewPath();
        }

        public List<Node> GetNewPath()
        {
            return GetNewPath(startCoordinates);
        }
        public List<Node> GetNewPath(Vector2Int coordinates)
        {
            gridManager.ResetNodes();
            BreadthFirstSearch(coordinates); 
            return BuildPath();
        }

        private void ExploreNeighbors()
        {
            List<Node> neighbors = new List<Node>();
            foreach (Vector2Int direction in directions)
            {
                Vector2Int neighborCoordinates = currentSearchNode.coordinates + direction;
                if (grid.TryFindNode(neighborCoordinates.x,neighborCoordinates.y))
                {
                    neighbors.Add(grid._gridArray[neighborCoordinates.x,neighborCoordinates.y]);

                }
            }
            foreach (Node neighbor in neighbors)
            {
                if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
                {
                    neighbor.connectedTo = currentSearchNode;
                    reached.Add(neighbor.coordinates,neighbor);
                    frontier.Enqueue(neighbor);
                }
            }
        }

        void BreadthFirstSearch(Vector2Int coordinates)
        {
            startNode.isWalkable = true;
            destinationNode.isWalkable = true;
        
            frontier.Clear();
            reached.Clear();
        
        
            bool isRunning = true;
            frontier.Enqueue(grid._gridArray[coordinates.x,coordinates.y]);
            reached.Add(coordinates,grid._gridArray[coordinates.x,coordinates.y]);
            while (frontier.Count >0 && isRunning)
            {
                currentSearchNode = frontier.Dequeue();
                currentSearchNode.isExplored = true;
                ExploreNeighbors();
                if (currentSearchNode.coordinates == destinationCoordinates)
                {
                    isRunning = false;
                }
            }
        }

        
        List<Node> BuildPath()
        {
            List<Node> path = new List<Node>();
            Node currentNode = destinationNode;
            path.Add(currentNode);
            currentNode.isPath = true;
            while (currentNode.connectedTo != null)
            {
                currentNode = currentNode.connectedTo;
                path.Add(currentNode);
                currentNode.isPath = true;
            }
            path.Reverse();
            return path;
        }

        public bool WillBlockPath(Vector2Int coordinates)
        {
            if (grid.TryFindNode(coordinates.x,coordinates.y))
            {
                bool previousState = grid._gridArray[coordinates.x,coordinates.y].isWalkable;
                grid._gridArray[coordinates.x,coordinates.y].isWalkable = false;
                List<Node> newPath = GetNewPath();
                grid._gridArray[coordinates.x,coordinates.y].isWalkable = previousState;

                if (newPath.Count <= 1)
                {
                    GetNewPath();
                    return true;
                }
            }
            return false;
        }

        public void NotifyReceivers()
        {
            BroadcastMessage("RecalculatePath",false,SendMessageOptions.DontRequireReceiver);
        }
    }
}