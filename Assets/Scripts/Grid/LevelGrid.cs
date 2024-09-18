using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    [SerializeField] private Transform gridDebugObjectPrefab;

    private Grid grid;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        grid = new Grid(10, 10, 2f);
        grid.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public void SetUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        grid.GetGridObject(gridPosition).SetUnit(unit);
    }

    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        return grid.GetGridObject(gridPosition).GetUnit();
    }

    public void ClearUnitAtGridPosition(GridPosition gridPosition)
    {
        grid.GetGridObject(gridPosition).SetUnit(null);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return grid.GetGridPosition(worldPosition);
    }
}
