using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    public Vector3 frame = new Vector3(50, 50, 50);

    [SerializeField] private int numberOfBoids = 20;
    [SerializeField] private Boid boidInstance;
    [SerializeField] private float maxSpeed = 1f;
    [SerializeField] private float borderBounce = 5f;
    [SerializeField] private float distance = 10f;
    [SerializeField] private float rule1 = 10000f;
    [SerializeField] private float rule2 = 100f;
    [SerializeField] private float rule3 = 80f;
    [SerializeField] private float ruleBorder = 80f;

    [HideInInspector] public List<Boid> boidList = new List<Boid>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfBoids; i++)
        {
            Vector3 startPos = new Vector3(Random.Range(0, frame.x), Random.Range(-1, frame.y -1), Random.Range(0, frame.z));
            Boid tempBoid = Instantiate(boidInstance, startPos, new Quaternion());
            tempBoid.velocity = Vector3.zero;
            boidList.Add(tempBoid);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v1, v2, v3, vBorder;
        foreach (Boid b in boidList)
        {
            v1 = Rule1(b);
            v2 = Rule2(b);
            v3 = Rule3(b);
            vBorder = Border(b);

            b.velocity = b.velocity + v1 + v2 + v3 + vBorder;
            LimitSpeed(b);
            b.position = b.position + b.velocity;
        }
    }

    void LimitSpeed(Boid b)
    {

        if (b.velocity.magnitude > maxSpeed)
        {
            b.velocity = (b.velocity / b.velocity.magnitude) * maxSpeed;
        }
    }

    Vector3 Rule1(Boid bJ)
    {
        Vector3 pcJ = Vector3.zero;

        foreach (Boid b in boidList)
        {
            if (b != bJ)
            {
                pcJ = pcJ + b.position;
            }
        }

        pcJ = pcJ / (boidList.Count - 1);

        return (pcJ - bJ.position) / rule1;
    }
    
    Vector3 Rule2(Boid bJ)
    {
        Vector3 c = Vector3.zero;

        foreach (Boid b in boidList)
        {
            if (b != bJ)
            {
                float dist = (b.position - bJ.position).magnitude;
                if (dist < distance)
                {
                    c = c - (b.position - bJ.position);
                }
            }
        }

        return c / rule2;
    }
    
    Vector3 Rule3(Boid bJ)
    {
        Vector3 pvJ = Vector3.zero;

        foreach (Boid b in boidList)
        {
            if (b != bJ)
            {
                pvJ = pvJ + b.velocity;
            }
        }

        pvJ = pvJ / (boidList.Count - 1);

        return (pvJ - bJ.velocity) / rule3;
    }

    Vector3 Border(Boid b)
    {
        int Xmin = 0, Ymin = 0, Zmin = 0;

        Vector3 v = Vector3.zero;

        if (b.position.x < Xmin)
        {
            v.x = borderBounce;
        }
        else if (b.position.x > frame.x)
        {
            v.x = -borderBounce;
        }

        if (b.position.y < Ymin)
        {
            v.y = borderBounce;
        }
        else if (b.position.y > frame.y)
        {
            v.y = -borderBounce;
        }

        if (b.position.z < Zmin)
        {
            v.z = borderBounce;
        }
        else if (b.position.z > frame.z)
        {
            v.z = -borderBounce;
        }

        return v / ruleBorder;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireCube(Vector3.zero + (frame * 0.5f), frame);
    //}
}
