using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private Grid grid;
    private GridPosition gridPosition;
    private List<Unit> unitList;

    public GridObject(Grid grid, GridPosition gridPosition)
    {
        this.grid = grid;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (Unit unit in unitList)
        {
            unitString += unit + "\n";
        }

        return gridPosition.ToString() + "\n" + unitString; 
    }
}
