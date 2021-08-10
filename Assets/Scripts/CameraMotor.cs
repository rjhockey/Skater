using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt; //penguin 
    public Vector3 offset = new Vector3(0, .5f, -10f);

    private void Start()
    {
        transform.position = lookAt.position + offset;
    }

    //called every frame
    private void LateUpdate() 
    {
        Vector3 desiredPosition = lookAt.position + offset;
        desiredPosition.x = 0;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
    }
}
