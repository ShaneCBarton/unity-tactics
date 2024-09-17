using UnityEngine;

public class Testing : MonoBehaviour
{
    private void Start()
    {
        new Grid(10, 10, 2f);

        Debug.Log(new GridPosition(5, 7));
    }
}
