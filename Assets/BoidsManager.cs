using System.Collections;
using System.Collections.Generic;
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

    private List<Boid> boidList = new List<Boid>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfBoids; i++)
        {
            Vector3 startPos = new Vector3(Random.Range(0, frame.x), Random.Range(0, frame.y), Random.Range(0, frame.z));
            Boid tempBoid = Instantiate(boidInstance, startPos, new Quaternion());
            tempBoid.velocity = new Vector3(Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed));
            boidList.Add(tempBoid);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveBoids();
    }

    void MoveBoids()
    {
        Vector3 v1, v2, v3, vBorder;
        foreach (Boid b in boidList)
        {
            v1 = Rule1(b);
            v2 = Rule2(b);
            v3 = Rule3(b);
            vBorder = Border(b);

            b.velocity = b.velocity + v1 + v2 + v3 + vBorder;
            //Debug.Log("Rule 1: " + v1 + "Rule 2: " + v2 + "Rule 3: "+ v3 * 100 + "Velocity: " + b.velocity);

            float x = Mathf.Clamp(b.velocity.x, -maxSpeed, maxSpeed);
            float y = Mathf.Clamp(b.velocity.y, -maxSpeed, maxSpeed);
            float z = Mathf.Clamp(b.velocity.z, -maxSpeed, maxSpeed);
            Vector3 clampedVelocity = new Vector3(x, y, z);
            b.position = b.position + clampedVelocity;

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
                float dist = Vector3.Distance(b.position, bJ.position);
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

        if (b.position.y < Zmin)
        {
            v.z = borderBounce;
        }
        else if (b.position.y > frame.z)
        {
            v.z = -borderBounce;
        }

        return v;
    }
}
