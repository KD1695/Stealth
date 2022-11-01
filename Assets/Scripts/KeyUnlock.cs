using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUnlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Key")
        {
            Destroy(other.gameObject);
        }
    }
}
