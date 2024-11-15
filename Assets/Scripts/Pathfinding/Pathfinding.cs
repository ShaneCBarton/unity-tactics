using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObjectPrefab;
    private int width;
    private int height;
    private float cellSize;
    private Grid<PathNode> grid;

    private void Awake()
    {
        grid = new Grid<PathNode>(10, 10, 2f, 
            (Grid<PathNode> gameObject, GridPosition gridPosition) => new PathNode(gridPosition));
        grid.CreateDebugObjects(gridDebugObjectPrefab);
    }
}
