using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.3F;
    public Transform targetxr;
    public Transform targetxl;

    private Vector3 currentVelocity;

    private void Update()
    {


        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target.position.x < targetxr.position.x && target.position.x > targetxl.position.x)
            {
                Vector3 newPos = new Vector3(target.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothSpeed);

            }
        }



}
