using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class AbilityAttacher : MonoBehaviour
{
    [SerializeField] GameObject ability;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameObject.Instantiate(ability, other.transform);
        }
    }
}
