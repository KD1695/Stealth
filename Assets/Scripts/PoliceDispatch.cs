using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceDispatch : MonoBehaviour
{
    [SerializeField] List<Transform> markers = new List<Transform>();

    public static PoliceDispatch Dispatch = null;
    
    void Awake()
    {
        if(Dispatch == null)
        {
            Dispatch = this;
        }
    }

    public Transform GetNextMarker(Transform marker)
    {
        var newlist = new List<Transform>();
        foreach(var _marker in markers)
        {
            if(!(Mathf.Abs(_marker.position.x-marker.position.x) > 10))
            {
                newlist.Add(_marker);
            }
            else if (!(Mathf.Abs(_marker.position.z - marker.position.z) > 10))
            {
                newlist.Add(_marker);
            }
        }
        return newlist[Random.Range(0, newlist.Count)];
    }
}
