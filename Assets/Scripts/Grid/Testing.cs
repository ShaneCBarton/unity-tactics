using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObjectPrefab;

    private Grid grid;

    private void Start()
    {
        grid = new Grid(10, 10, 2f);
        grid.CreateDebugObjects(gridDebugObjectPrefab);
    }

}
