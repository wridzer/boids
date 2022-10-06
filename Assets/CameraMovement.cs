using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private BoidsManager boidsManager;
    [SerializeField] private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pcJ = Vector3.zero;

        foreach (Boid b in boidsManager.boidList)
        {
            pcJ = pcJ + b.position;
        }

        pcJ = pcJ / boidsManager.boidList.Count;

        Vector3 lookDir = Vector3.RotateTowards(transform.forward, pcJ, 1 * Time.deltaTime, 0f);
        transform.rotation = Quaternion.Euler(lookDir);
        transform.position = pcJ + offset;
    }
}
