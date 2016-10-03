using UnityEngine;
using System.Collections;

public class Frisbee : MonoBehaviour
{
    public Vector3 velocity;        //velocity and also direction
    public float AoA;          //Angle of attack
    public float A0;          //initial angle

    private float time;             //elapsed time from start of the throw
    private Vector3 startPosition;  //position at start of the throw

    private const float mass = 0.175f;      //standard weight for frisbee according to USA ultimate
    private const float area = 0.0568f;     //standard area                -  ||  -
    private const float Cl_0 = 0.1f;        //Cl at angle 0
    private const float Cd_0 = 0.08f;       //Cd at angle 0
    private const float Cl_a = 1.4f;        //Cl dependent on angle
    private const float Cd_a = 2.72f;       //Cd dependent on angle
    private const float g = 9.82f;          //Gravitational constant
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
        float Cl = Cl_0 + Cl_a * (AoA * Mathf.PI / 180); // Cl = Cl0 + Cl(angle)
        float Cd = Cd_0 + Cd_a * (Mathf.Pow((AoA - A0) * Mathf.PI / 180, 2));
        float Fl = airDensity * area * Cl * this.velocity.sqrMagnitude * 0.5f;  //F = ½pACv^2
        float Fd = airDensity * area * Cd * this.velocity.sqrMagnitude * 0.5f;  //  -||-

        // transform.position = startPosition + this.velocity * time + new Vector3(0, -g * time * time * 0.5f, 0);//position with gravity only
        this.velocity = this.velocity + new Vector3(-Fd * dt, (Fl - g) * dt, -Fd * dt); //change to being negative compared to current x and z speeds
        transform.position = transform.position + this.velocity * dt;

        transform.Rotate(new Vector3(0, 100, 0) * dt);//TEST: denna gör rotation med 90 grader per sekund



        //ass
    }
}

