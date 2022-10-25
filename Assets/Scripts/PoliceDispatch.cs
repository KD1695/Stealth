using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum PoliceStates
{
    Patrol,
    Chase,
    Detect
}

public class PoliceDispatch : MonoBehaviour
{
    [SerializeField] List<Transform> markers = new List<Transform>();
    [SerializeField] PlayerMovement player;
    [SerializeField] GameObject topViewCam;

    public static PoliceDispatch Dispatch = null;
    public static event Action<PoliceStates> alertAll;
    
    void Awake()
    {
        if(Dispatch == null)
        {
            Dispatch = this;
        }
    }

    List<Transform> GetAdjacentMarkers(Transform transform)
    {
        var newlist = new List<Transform>();
        foreach (var _marker in markers)
        {
            if (!(Mathf.Abs(_marker.position.x - transform.position.x) > 10))
            {
                newlist.Add(_marker);
            }
            else if (!(Mathf.Abs(_marker.position.z - transform.position.z) > 10))
            {
                newlist.Add(_marker);
            }
        }
        return newlist;
    }

    public Transform GetNextMarker(Transform marker)
    {
        var newlist = GetAdjacentMarkers(marker);
        return newlist[UnityEngine.Random.Range(0, newlist.Count)];
    }

    public List<Transform> GetPathToPlayer(Transform marker)
    {
        List<Transform> path = new List<Transform>();
        Transform closestMarker = marker;

        while(Mathf.Abs(Vector3.Distance(closestMarker.position, player.transform.position)) > 10)
        {
            var adjacentMarkers = GetAdjacentMarkers(closestMarker);
            float minDistance = 999f;
            foreach(var _marker in adjacentMarkers)
            {
                var newDist = Mathf.Abs(Vector3.Distance(_marker.position, player.transform.position));
                if (minDistance > newDist)
                {
                    minDistance = newDist;
                    closestMarker = _marker;
                }
            }
            if(closestMarker != null)
            {
                path.Add(closestMarker);
            }
        }
        path.Add(player.transform);

        return path;
    }

    public void FoundPlayer()
    {
        alertAll(PoliceStates.Chase);
    }

    public void StartPatrol()
    {
        alertAll(PoliceStates.Patrol);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            topViewCam.SetActive(!topViewCam.activeSelf);
        }
    }
}
