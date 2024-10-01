using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : MonoBehaviour
{
    private const float MAX_ROTATION_AMOUNT = -35f;
    [SerializeField] private float flySpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float zRotation;
    [SerializeField] private float explosionDistance;
    [SerializeField] private GameObject explosionFX;

    private Vector3 rotation;

    private void Update()
    {
        transform.Translate(Vector3.forward * flySpeed * Time.deltaTime);

        rotation += new Vector3(0, 0, zRotation * rotateSpeed * Time.deltaTime);
        rotation.z = Mathf.Clamp(rotation.z, MAX_ROTATION_AMOUNT, 0);
        transform.eulerAngles = new Vector3(rotation.x, 25, rotation.z);

        ExplodeShip();
    }

    private void ExplodeShip()
    {
        if (transform.position.z >= explosionDistance)
        {
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(gameObject);    
        }
    }
}
