using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance {  get; private set; }

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    [SerializeField] private Transform gridDebugObjectPrefab;
    private int width;
    private int height;
    private float cellSize;
    private Grid<PathNode> grid;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        grid = new Grid<PathNode>(10, 10, 2f, 
            (Grid<PathNode> gameObject, GridPosition gridPosition) => new PathNode(gridPosition));
        grid.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public List<GridPosition> FindPath(GridPosition startPosition, GridPosition endPosition)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        PathNode startNode = grid.GetGridObject(startPosition);
        PathNode endNode = grid.GetGridObject(endPosition);
        openList.Add(startNode);

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int z =  0; z < grid.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                PathNode pathNode = grid.GetGridObject(gridPosition);

                pathNode.SetGCost(int.MaxValue);
                pathNode.SetHCost(int.MaxValue);
                pathNode.CalculateFCost();
                pathNode.ResetCameFromPathNode();
            }
        }

        startNode.SetGCost(0);
        startNode.SetHCost(CalculateDistance(startPosition, endPosition));
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostPathNode(openList);

            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }

                int tentativeGCost = currentNode.GetGCost() + CalculateDistance(currentNode.GetGridPosition(), neighbourNode.GetGridPosition());
                if(tentativeGCost < neighbourNode.GetGCost())
                {
                    neighbourNode.SetCameFromPathNode(currentNode);
                    neighbourNode.SetGCost(tentativeGCost);
                    neighbourNode.SetHCost(CalculateDistance(neighbourNode.GetGridPosition(), endPosition));
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // no path
        return null;
    }
    public int CalculateDistance(GridPosition gridPositionA, GridPosition gridPositionB)
    {
        GridPosition gridPositionDistance = gridPositionA - gridPositionB;
        int totalDistance = (Mathf.Abs(gridPositionDistance.x) + Mathf.Abs(gridPositionDistance.z));
        int xDistance = Mathf.Abs(gridPositionDistance.x);
        int zDistance = Mathf.Abs(gridPositionDistance.z);
        int remaining = Mathf.Abs(xDistance - zDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, xDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostPathNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostPathNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].GetFCost() < lowestFCostPathNode.GetFCost())
            {
                lowestFCostPathNode = pathNodeList[i];
            }
        }

        return lowestFCostPathNode;
    }

    private PathNode GetNode(int x, int z)
    {
        return grid.GetGridObject(new GridPosition(x, z));
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        GridPosition gridPosition = currentNode.GetGridPosition();

        if (gridPosition.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 0));
            if (gridPosition.z - 1 >= 0)
            {
                // Left Down
                neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z - 1));
            }

            if (gridPosition.z + 1 < grid.GetHeight())
            {
                // Left Up
                neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 1));
            }
        }

        if (gridPosition.x + 1 < grid.GetWidth())
        {
            // Right
            neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 0));
            if (gridPosition.z - 1 >= 0)
            {
                // Right Down
                neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z - 1));
            }
            if (gridPosition.z + 1 < grid.GetHeight())
            {
                // Right Up
                neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 1));
            }
        }

        if (gridPosition.z - 1 >= 0)
        {
            // Down
            neighbourList.Add(GetNode(gridPosition.x + 0, gridPosition.z - 1));
        }
        if (gridPosition.z + 1 < grid.GetHeight())
        {
            // Up
            neighbourList.Add(GetNode(gridPosition.x + 0, gridPosition.z + 1));
        }

        return neighbourList;
    }

    private List<GridPosition> CalculatePath(PathNode endNode)
    {
        List<PathNode> pathNodeList = new List<PathNode>();
        pathNodeList.Add(endNode);
        PathNode currentNode = endNode;

        while (currentNode.GetCameFromPathNode() != null)
        {
            pathNodeList.Add(currentNode.GetCameFromPathNode());
            currentNode = currentNode.GetCameFromPathNode();
        }

        pathNodeList.Reverse();
        List<GridPosition> gridPositions = new List<GridPosition>();
        foreach (PathNode pathNode in pathNodeList)
        {
            gridPositions.Add(pathNode.GetGridPosition());
        }

        return gridPositions;
    }
}
