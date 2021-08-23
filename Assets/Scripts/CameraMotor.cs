using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt; //penguin 
    public Vector3 offset = new Vector3(0, .5f, -10f);
    public Vector3 rotation = new Vector3(35, 0, 0);

    public bool IsMoving { set; get; }

    //remove for intro animation
    //private void Start()
    //{
    //    transform.position = lookAt.position + offset;
    //}

    //called every frame
    private void LateUpdate() 
    {
        //for intro animation
        if (!IsMoving)
            return; //skip code below

        Vector3 desiredPosition = lookAt.position + offset;
        desiredPosition.x = 0;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.1f);
    }
}
