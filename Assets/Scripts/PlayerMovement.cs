using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody body;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rotationSpeed = 5f;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            body.velocity = this.gameObject.transform.forward * movementSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            body.velocity = this.gameObject.transform.forward * movementSpeed * -1;
        }
        else
        {
            body.velocity = new Vector3(0, 0, 0);
        }

        float step = Time.deltaTime * rotationSpeed;
        if(Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(new Vector3(0, -step, 0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(new Vector3(0, step, 0));
        }
    }
}
