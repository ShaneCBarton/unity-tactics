using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxMoveDistance = 4;

    private float rotationSpeed = 10f;
    private Vector3 targetPosition; 
    private float stoppingDistance = .1f;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (isActive)
        {
            PollUnitMovement();
        }
    }

    private void PollUnitMovement()
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            transform.position += moveDirection * Time.deltaTime * moveSpeed;

            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
            isActive = false;
            onActionComplete();
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        isActive = true;
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++) 
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testgridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testgridPosition))
                {
                    continue;
                }

                if (unitGridPosition == testgridPosition)
                {
                    continue;
                }

                if (LevelGrid.Instance.HasUnitOnGridPosition(testgridPosition))
                {
                    continue;
                }

                validGridPositionList.Add(testgridPosition);
            }
        }

        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }
}
