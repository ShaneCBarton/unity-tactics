using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator unitAnimator;

    private float stoppingDistance = .1f;
    private float rotationSpeed = 10f;
    private Vector3 targetPosition;

    private void Update()
    {
        PollMouseInput();
        PollUnitMovement();
    }

    private void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private void PollMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Move(MouseWorld.GetPosition());
        }
    }

    private void PollUnitMovement()
    {
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
            transform.position += moveDirection * Time.deltaTime * moveSpeed;

            unitAnimator.SetBool("IsWalking", true);
        } 
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }
    }
}
