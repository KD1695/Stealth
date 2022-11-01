using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

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
    [SerializeField] GameObject cctvParentObject;

    public static PoliceDispatch Dispatch = null;
    public static event Action<PoliceStates> alertAll;
    PoliceStates currentState = PoliceStates.Patrol;
    float timeSinceLastSeen = 100f;
    float timeSinceCCTVOff = 0f;

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

    public List<Vector3> GetPathToPlayer(Transform marker)
    {
        List<Vector3> path = new List<Vector3>();
        Transform closestMarker = marker;
        Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        while (Mathf.Abs(Vector3.Distance(closestMarker.position, playerPosition)) > 15 && path.Count < 40)
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
                path.Add(closestMarker.position);
            }
        }
        path.Add(playerPosition);

        return path;
    }

    public void FoundPlayer()
    {
        timeSinceLastSeen = 0;
        currentState = PoliceStates.Chase;
        alertAll(currentState);
    }

    public void CaughtPlayer()
    {
        //gameOverScreen
        Debug.LogError("GameOver!!!!!!");
    }

    public void StartPatrol()
    {
        currentState = PoliceStates.Patrol;
        alertAll(currentState);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            topViewCam.SetActive(!topViewCam.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //restart
            SceneManager.LoadSceneAsync(0);
        }

        if (timeSinceLastSeen > 5.0f && currentState != PoliceStates.Patrol)
        {
            StartPatrol();
        }

        if(timeSinceCCTVOff > 5 && !cctvParentObject.activeSelf)
        {
            EnableCCTVs(true);
        }

        timeSinceLastSeen += Time.deltaTime;
        timeSinceCCTVOff += Time.deltaTime;
    }

    public void EnableCCTVs(bool isEnabled)
    {
        cctvParentObject.SetActive(isEnabled);
        if(!isEnabled)
        {
            timeSinceCCTVOff = 0;
        }
    }
}
