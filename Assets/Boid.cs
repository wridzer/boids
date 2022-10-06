using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector3 velocity { get; set; }
    public Vector3 position {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        } 
    }

    private void Update()
    {
        Vector3 lookDir = Vector3.RotateTowards(transform.forward, velocity.normalized, 5 * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(lookDir);
    }

}
