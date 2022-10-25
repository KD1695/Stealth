using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    Transform targetMarker = null;

    PoliceStates currentState = PoliceStates.Patrol;
    List<Transform> pathToPlayer = null;

    void Start()
    {
        if(targetMarker == null)
        {
            targetMarker = PoliceDispatch.Dispatch.GetNextMarker(this.transform);
            transform.rotation = Quaternion.LookRotation(targetMarker.position - transform.position);
        }

        PoliceDispatch.alertAll += SwitchState;
    }

    void Update()
    {
        var step = movementSpeed * Time.deltaTime;
        switch(currentState)
        {
            case PoliceStates.Patrol:
                {
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
                    transform.position = Vector3.MoveTowards(transform.position, targetMarker.position, step);

                    if (Vector3.Distance(transform.position, targetMarker.position) < 0.001f)
                    {
                        if(pathToPlayer.Count > 0)
                        {
                            targetMarker = pathToPlayer[0];
                            pathToPlayer.RemoveAt(0);
                            transform.rotation = Quaternion.LookRotation(targetMarker.position - transform.position);
                        }
                    }
                    break;
                }
        }
    }

    public void SwitchState(PoliceStates newState)
    {
        if (currentState == newState)
            return;

        switch(newState)
        {
            case PoliceStates.Patrol:
                {
                    currentState = PoliceStates.Patrol;
                    pathToPlayer = null;
                    break;
                }

            case PoliceStates.Chase:
                {
                    currentState = PoliceStates.Chase;
                    pathToPlayer = PoliceDispatch.Dispatch.GetPathToPlayer(transform);
                    targetMarker = pathToPlayer[0];
                    pathToPlayer.RemoveAt(0);
                    break;
                }
        }
    }
}