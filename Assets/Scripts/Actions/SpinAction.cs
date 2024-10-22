using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpinAction : BaseAction
{
    private float totalSpinAmount;

    private void Update()
    {
        if (isActive)
        {
            float spinAmount = 360f * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinAmount, 0);
            totalSpinAmount += spinAmount;
            if (totalSpinAmount >= 360f)
            {
                isActive = false;
                onActionComplete();
            }
        }
    }

    public void Spin(Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        isActive = true;
        totalSpinAmount = 0f;
    }
}
