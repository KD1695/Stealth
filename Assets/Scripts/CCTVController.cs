using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 3f;
    int stepDirection = 1;

    void Update()
    {
        var step = rotationSpeed * Time.deltaTime * stepDirection;
        transform.Rotate(new Vector3(0, 0, step));
        var angle180 = (this.transform.rotation.eulerAngles.z > 180) ? this.transform.rotation.eulerAngles.z - 360 : this.transform.rotation.eulerAngles.z;
        if (Mathf.Abs(angle180) > 16)
        {
            stepDirection *= -1;
        }
    }
}
