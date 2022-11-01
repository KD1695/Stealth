using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVAbility : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            PoliceDispatch.Dispatch.EnableCCTVs(false);
        }
    }
}
