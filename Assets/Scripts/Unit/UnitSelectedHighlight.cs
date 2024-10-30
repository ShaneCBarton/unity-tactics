using System;
using UnityEngine;

public class UnitSelectedHighlight : MonoBehaviour
{
    [SerializeField] Unit unit;
    [SerializeField] GameObject lightShaft;

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitManager_OnSelectedUnitChanged;
        UpdateVisual();
    }

    private void UnitManager_OnSelectedUnitChanged(object sender, EventArgs empty)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
        {
            lightShaft.SetActive(true);
        } else
        {
            lightShaft.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged -= UnitManager_OnSelectedUnitChanged;

    }
}
