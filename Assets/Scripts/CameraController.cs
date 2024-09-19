using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        Vector3 inputMoveDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDirection.z = +1f;
        }       
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDirection.z = -1f;
        }        
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDirection.x = -1f;
        }        
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDirection.x = +1f;
        }

        float moveSpeed = 10f;
        Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }
}
