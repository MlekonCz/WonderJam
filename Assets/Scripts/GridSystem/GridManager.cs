using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] Vector2Int gridSize;
        [SerializeField] private int unityGridSize = 10;
        [SerializeField] private int _CellSize = 5;
        public int UnityGridSize => unityGridSize;


        private Grid grid;


        //Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

        public Grid Grid
        {
            get { return grid; }
        }

        private void Awake()
        {
           // CreateGrid();
           grid = new Grid(_CellSize, _CellSize, unityGridSize, new Vector3(0, 0));
        }


        private void Start()
        {
        }

        public Node GetNode(Vector2Int coordinates)
        {
            if (grid.TryFindNode(coordinates.x,coordinates.y))
            {
                return grid._gridArray[coordinates.x,coordinates.y];
            }

            return null;
        }

        public void BlockNode(Vector2Int coordinates)
        {
            if (grid.TryFindNode(coordinates.x,coordinates.y))
            {
                grid._gridArray[coordinates.x,coordinates.y].isWalkable = false;
            }
        }

        public void ResetNodes()
        {
            foreach (var entry in grid._gridArray)
            {
                entry.connectedTo = null;
                entry.isExplored = false;
                entry.isPath = false;
            }
        }


        public Vector2Int GetCoordinatesFromPosition(Vector3 position)
        {
            Vector2Int coordinates = new Vector2Int();
            coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
            coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);
            return coordinates;
        }

        public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
        {
            return grid.GetWorldPosition(coordinates.x,coordinates.y) + new Vector3(unityGridSize, unityGridSize) / 2;
        }

        // private void CreateGrid()
        // {
        //     for (int x = 0; x < gridSize.x; x++)
        //     {
        //         for (int y = 0; y < gridSize.y; y++)
        //         {
        //             Vector2Int coordinates = new Vector2Int(x, y);
        //             grid.Add(coordinates, new Node(coordinates, true));
        //         }
        //     }
        // }
    }
}