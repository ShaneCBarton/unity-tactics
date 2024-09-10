using UnityEngine;

public class MouseWorld : MonoBehaviour
{

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit);
        transform.position = raycastHit.point;
    }
}
