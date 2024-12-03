using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootAction : BaseAction
{
    [SerializeField] private AudioSource soundObj;

    public event EventHandler<OnShootEventArgs> OnShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Unit targetUnit;
        public Unit shootingUnit;
    }

    private enum State
    {
        Aiming,
        Shooting,
        Cooldown
    }

    [SerializeField] private LayerMask obstacleLayerMask;

    private int maxShootDistance = 7;
    private State state;
    private float stateTimer;
    private Unit targetUnit;
    private bool canShootBullet;

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        stateTimer -= Time.deltaTime;
        switch (state)
        {
            case State.Aiming:
                Vector3 aimDirection = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;
                float rotationSpeed = 10f;
                transform.forward = Vector3.Slerp(transform.forward, aimDirection, Time.deltaTime * rotationSpeed);
                break;
            case State.Shooting:
                if (canShootBullet)
                {
                    Shoot();
                    canShootBullet = false;
                }
                break;
            case State.Cooldown:
                break;
        }

        if (stateTimer <= 0f)
        {
            NextState();
        }
    }


    private void NextState()
    {
        switch (state)
        {
            case State.Aiming:
                state = State.Shooting;
                float shootingTime = 0.1f;
                stateTimer = shootingTime;
                break;
            case State.Shooting:
                state = State.Cooldown;
                float cooldownStateTime = 0.5f;
                stateTimer = cooldownStateTime;
                break;
            case State.Cooldown:
                ActionComplete();
                break;
        }
    }
    private void Shoot()
    {
        OnShoot?.Invoke(this, new OnShootEventArgs
        {
            targetUnit = this.targetUnit,
            shootingUnit = unit
        });
        soundObj.Play();
        targetUnit.Damage(40);
    }

    public override string GetActionName()
    {
        return "Shoot";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();
        return GetValidActionGridPositionList(unitGridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testgridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testgridPosition))
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxShootDistance)
                {
                    continue;
                }

                if (!LevelGrid.Instance.HasUnitOnGridPosition(testgridPosition))
                {
                    continue;
                }

                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testgridPosition);

                if (targetUnit.IsEnemy() == unit.IsEnemy())
                {
                    continue;
                }

                Vector3 unitWorldPosition = LevelGrid.Instance.GetWorldPosition(unitGridPosition);
                Vector3 shootDirection = (targetUnit.GetWorldPosition() - unitWorldPosition).normalized;
                float unitSHoulderHeight = 1.7f;
                if (Physics.Raycast(unit.GetWorldPosition() + Vector3.up * unitSHoulderHeight,
                        shootDirection,
                        Vector3.Distance(unitWorldPosition, targetUnit.GetWorldPosition()),
                        obstacleLayerMask))
                {
                    // LOS blocked
                    continue;
                }

                validGridPositionList.Add(testgridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;

        canShootBullet = true;

        ActionStart(onActionComplete);
    }

    public Unit GetTargetUnit()
    {
        return targetUnit;
    }

    public int GetMaxShootDistance()
    {
        return maxShootDistance;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 100 + Mathf.RoundToInt((1 - targetUnit.GetHealthNormalized()) * 100f),
        };
    }

    public int GetTargetCountAtPosition(GridPosition gridPosition)
    {
        return GetValidActionGridPositionList(gridPosition).Count;
    }
}
