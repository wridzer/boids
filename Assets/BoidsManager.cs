using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    public Vector3 frame = new Vector3(1000, 1000, 1000);

    [SerializeField] private int numberOfBoids = 20;
    [SerializeField] private Boid boidInstance;

    private List<Boid> boidList = new List<Boid>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfBoids; i++)
        {
            Vector3 startPos = new Vector3(Random.Range(0, frame.x), Random.Range(0, frame.y), Random.Range(0, frame.z));
            Boid tempBoid = Instantiate(boidInstance, startPos, new Quaternion());
            boidList.Add(tempBoid);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DrawBoids();
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
            b.position = b.position + b.velocity;
        }
    }

    Vector3 Rule1(Boid bJ)
    {
        Vector3 pcJ = new Vector3();

        foreach (Boid b in boidList)
        {
            if (b != bJ)
            {
                pcJ = pcJ + b.position;
            }
        }

        pcJ = pcJ / (boidList.Count - 1);

        return (pcJ - bJ.position) / 100;
    }
    
    Vector3 Rule2(Boid bJ)
    {
        Vector3 c = new Vector3();

        foreach (Boid b in boidList)
        {
            if (b != bJ)
            {
                float dist = Vector3.Distance(b.position, bJ.position);
                if (dist < 100)
                {
                    c = c - (b.position - bJ.position);
                }
            }
        }

        return c;
    }
    
    Vector3 Rule3(Boid bJ)
    {
        Vector3 pvJ = new Vector3();

        foreach (Boid b in boidList)
        {
            if (b != bJ)
            {
                pvJ = pvJ + b.velocity;
            }
        }

        pvJ = pvJ / (boidList.Count - 1);

        return (pvJ - bJ.velocity) / 8;
    }

    Vector3 Border(Boid b)
    {
        int Xmin = 0, Ymin = 0, Zmin = 0;

        Vector3 v = new Vector3();

        if (b.position.x < Xmin)
        {
            v.x = 10;
        }
        else if (b.position.x > frame.x)
        {
            v.x = -10;
        }

        if (b.position.y < Ymin)
        {
            v.y = 10;
        }
        else if (b.position.y > frame.y)
        {
            v.y = -10;
        }

        if (b.position.y < Zmin)
        {
            v.z = 10;
        }
        else if (b.position.y > frame.z)
        {
            v.z = -10;
        }

        return v;
    }

    void DrawBoids()
    {
        foreach (Boid b in boidList)
        {
            b.Draw();
            b.position = b.transform.position;
        }
    }
}
