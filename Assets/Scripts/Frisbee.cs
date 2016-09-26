using UnityEngine;
using System.Collections;

public class Frisbee : MonoBehaviour
{
    public Vector3 velocity;//velocity and also direction


    private float time;             //elapsed time from start of the throw
    private Vector3 startPosition;  //position at start of the throw

    private const float gravetyConstant = 9.82f;

    private const float airDensity = 1.23f; //average airdensity at sealevel (kg/m^3)

    // Use this for initialization
    void Start()
    {
        this.time = 0;
        this.startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        time += dt;

        transform.position = startPosition + this.velocity * time + new Vector3(0, -gravetyConstant * time * time * 0.5f, 0);//position with gravety only

        transform.Rotate(new Vector3(0, 90, 0) * dt);//TEST: denna gör rotation med 90 grader per sekund


        //ass
    }
}
