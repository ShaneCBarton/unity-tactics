using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private Grid grid;
    private GridPosition gridPosition;

    public GridObject(Grid grid, GridPosition gridPosition)
    {
        this.grid = grid;
        this.gridPosition = gridPosition;
    }
}
