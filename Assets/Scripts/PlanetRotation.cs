using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float rotationSpeed;
    private Vector3 rotation;

    private void Update()
    {
        rotation += new Vector3(x, y, 0) * rotationSpeed * Time.deltaTime;
        transform.eulerAngles = rotation;
    }
}
