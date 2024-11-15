using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    public event EventHandler OnAnyUnitMovedGridPosition;

    [SerializeField] private Transform gridDebugObjectPrefab;

    private Grid<GridObject> grid;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        grid = new Grid<GridObject>(10, 10, 2f, (Grid<GridObject> g, GridPosition gridPosition) => new GridObject(g, gridPosition));
        grid.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        grid.GetGridObject(gridPosition).AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
    {
        return grid.GetGridObject(gridPosition).GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        grid.GetGridObject(gridPosition).RemoveUnit(unit);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return grid.GetGridPosition(worldPosition);
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return grid.GetWorldPosition(gridPosition);
    }

    public int GetWidth() { return grid.GetWidth(); }

    public int GetHeight() {  return grid.GetHeight(); }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return grid.IsValidGridPosition(gridPosition);
    }


    public bool HasUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = grid.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition fromPosition, GridPosition toPosition)
    {
        RemoveUnitAtGridPosition(fromPosition, unit);
        AddUnitAtGridPosition(toPosition, unit);
        OnAnyUnitMovedGridPosition?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = grid.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }

}
