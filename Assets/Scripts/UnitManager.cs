using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private Unit selectedUnit;

    private void Update()
    {
        PollMouseInput();
    }

    private void PollMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedUnit.Move(MouseWorld.GetPosition());
        }
    }
}
