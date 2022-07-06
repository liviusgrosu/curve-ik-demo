using UnityEngine;

//Attach this script to a GameObject to rotate around the target position.
public class RotateAround : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    
    void Update()
    {
        // Spin the object around the target at 20 degrees/second.
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotationSpeed);
    }
}