using System;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        PollMouseInput();
    }

    private void PollMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) { return; }
            selectedUnit.Move(MouseWorld.GetPosition());
        }
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;

        if (OnSelectedUnitChanged != null)
        {
            OnSelectedUnitChanged(this, EventArgs.Empty);
        }
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
