using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleLifebar : HealthBar
{
    [SerializeField] Transform mainCameraTf;
    [SerializeField] bool rotateToward;
    [SerializeField] float rotX, rotY, rotZ;

    private void Update()
    {
        if (rotateToward)
        {
            holder.transform.rotation = Quaternion.Euler(new Vector3(rotX, rotY, holder.transform.rotation.z));
        }
    }
}
