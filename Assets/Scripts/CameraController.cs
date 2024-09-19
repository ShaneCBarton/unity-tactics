using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private const float MIN_FOLLOW_OFFSET = 2f;
    private const float MAX_FOLLOW_OFFSET = 12f;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    void Update()
    {
        Vector3 inputMoveDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDirection.z = 1f;
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
            inputMoveDirection.x = 1f;
        }

        float moveSpeed = 10f;
        Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;

        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = 1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        float rotationSpeed = 100f;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;

        CinemachineTransposer transposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        Vector3 followOffset = transposer.m_FollowOffset;
        float zoomAmount = 1f;
        if (Input.mouseScrollDelta.y > 0)
        {
            followOffset.y -= zoomAmount;
        }       
        if (Input.mouseScrollDelta.y < 0)
        {
            followOffset.y += zoomAmount;
        }
        followOffset.y = Mathf.Clamp(followOffset.y, MIN_FOLLOW_OFFSET, MAX_FOLLOW_OFFSET);
        transposer.m_FollowOffset = followOffset;
    }
}
