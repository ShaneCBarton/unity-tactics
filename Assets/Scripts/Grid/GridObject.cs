using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private Grid grid;
    private GridPosition gridPosition;
    private Unit unit;

    public GridObject(Grid grid, GridPosition gridPosition)
    {
        this.grid = grid;
        this.gridPosition = gridPosition;
    }

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
    }

    public Unit GetUnit()
    {
        return unit;
    }

    public override string ToString()
    {
        return gridPosition.ToString() + "\n" + unit; 
    }
}
