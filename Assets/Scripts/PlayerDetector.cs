using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("FOUND PLAYER!!!!");
            PoliceDispatch.Dispatch.FoundPlayer();
        }
    }
}
