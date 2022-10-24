using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    Transform targetMarker = null;

    PoliceStates currentState = PoliceStates.Patrol;

    void Start()
    {
        if(targetMarker == null)
        {
            targetMarker = PoliceDispatch.Dispatch.GetNextMarker(this.transform);
            transform.rotation = Quaternion.LookRotation(targetMarker.position - transform.position);
        }
    }

    void Update()
    {
        switch(currentState)
        {
            case PoliceStates.Patrol:
                {
                    var step = movementSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, targetMarker.position, step);

                    if (Vector3.Distance(transform.position, targetMarker.position) < 0.001f)
                    {
                        targetMarker = PoliceDispatch.Dispatch.GetNextMarker(this.transform);
                        transform.rotation = Quaternion.LookRotation(targetMarker.position - transform.position);
                    }
                    break;
                }

            case PoliceStates.Chase:
                {
                    break;
                }
        }
    }
}
