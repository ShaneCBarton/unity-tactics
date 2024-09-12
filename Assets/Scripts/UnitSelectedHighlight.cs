using System;
using UnityEngine;

public class UnitSelectedHighlight : MonoBehaviour
{
    [SerializeField] Unit unit;
    [SerializeField] GameObject lightShaft;

    private void Start()
    {
        UnitManager.Instance.OnSelectedUnitChanged += UnitManager_OnSelectedUnitChanged;
        UpdateVisual();
    }

    private void UnitManager_OnSelectedUnitChanged(object sender, EventArgs empty)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (UnitManager.Instance.GetSelectedUnit() == unit)
        {
            lightShaft.SetActive(true);
        } else
        {
            lightShaft.SetActive(false);
        }
    }
}
