using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    Vector3 targetMarker = Vector3.zero;

    PoliceStates currentState = PoliceStates.Patrol;
    List<Vector3> pathToPlayer = new List<Vector3>();

    void Start()
    {
        if(targetMarker == Vector3.zero)
        {
            targetMarker = PoliceDispatch.Dispatch.GetNextMarker(this.transform).position;
            transform.rotation = Quaternion.LookRotation(targetMarker - transform.position);
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
                    transform.position = Vector3.MoveTowards(transform.position, targetMarker, step);

                    if (Vector3.Distance(transform.position, targetMarker) < 0.001f)
                    {
                        targetMarker = PoliceDispatch.Dispatch.GetNextMarker(this.transform).position;
                        transform.rotation = Quaternion.LookRotation(targetMarker - transform.position);
                    }
                    break;
                }

            case PoliceStates.Chase:
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetMarker, step);

                    if (Vector3.Distance(transform.position, targetMarker) < 0.001f)
                    {
                        if(pathToPlayer.Count > 0)
                        {
                            targetMarker = pathToPlayer[0];
                            pathToPlayer.RemoveAt(0);
                            transform.rotation = Quaternion.LookRotation(targetMarker - transform.position);
                        }
                        else
                        {
                            pathToPlayer = PoliceDispatch.Dispatch.GetPathToPlayer(this.transform);
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
                    pathToPlayer.Clear();
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