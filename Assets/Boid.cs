using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector3 velocity { get; set; }
    public Vector3 position { get; set; }
    [SerializeField] private float maxSpeed = 1f;

    public void Draw()
    {
        transform.position = Vector3.MoveTowards(transform.position, position * Time.deltaTime, maxSpeed);
    }
}
