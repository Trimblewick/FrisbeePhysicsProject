using UnityEngine;
using System.Collections;


public class Frisbee : MonoBehaviour
{
    public Vector3 velocity;        //velocity and also direction
    public Vector3 spin;            //Speed of rotation
    public float AoA;               //Angle of attack
    public float A0;                //constant dependent of frisbee make
    public bool idle;
    
    private Vector3 startPosition;  //position at start of the throw

    private const float mass = 0.175f;      //standard weight for frisbee according to USA ultimate
    private const float area = 0.0568f;     //standard area                -  ||  -
    private const float radius = 0.1345f;   //standard radius
    private const float height = 0.02f;     //test height
    private const float Cl_0 = 0.1f;        //Cl at angle 0
    private const float Cd_0 = 0.08f;       //Cd at angle 0
    private const float Cl_a = 1.0f;        //Cl dependent on angle
    private const float Cd_a = 2.72f;       //Cd dependent on angle
    private const float g = 9.82f;          //Gravitational constant
    private const float airDensity = 1.23f; //average air density at sealevel (kg/m^3)
    private Vector3 wind;

    public float getRadius()
    {
        return radius;
    }
	// Use this for initialization
	void Start () {
        this.spin = -this.spin;
        this.idle = false;
        this.wind = new Vector3((Random.Range(-5f, 5f)), (Random.Range(-5f, 5f))*0.1f, (Random.Range(-5f, 5f)));
    }
	
	// Update is called once per frame
	void Update () {
        if (!idle)
        {
            float dt = Time.deltaTime;
            Vector3 plane = new Vector3(velocity.x, 0, velocity.z);
            this.AoA = Mathf.Acos(Vector3.Dot(plane.normalized, velocity.normalized));
            Debug.Log(AoA);
            Vector3 u = this.velocity - this.wind;
            float Cl = Cl_0 + Cl_a * (AoA * Mathf.PI / 180); // Cl = Cl0 + Cl(angle)
            float Cd = Cd_0 + Cd_a * (Mathf.Pow((AoA - A0) * Mathf.PI / 180, 2));
            float Fl = airDensity * area * Cl * u.sqrMagnitude * 0.5f;  //F = ½pACv^2
            float Fd = airDensity * area * Cd * u.sqrMagnitude * 0.5f;  //  -||-
            float G = 2 * Mathf.PI * Mathf.Pow(radius, 2) * spin.y * Mathf.PI / 180; //vortex established by rotation
            float Fm = airDensity * Mathf.Sqrt(velocity.sqrMagnitude) * G * height; //F = density * v * G * l for a cylinder according to NASA
                                                                                    //note: multiplying with PI / 180 converts degrees to radians

            Vector3 vFd = -(Fd * u.normalized); // -Fd * ev
            Vector3 drag = vFd * dt / mass; //drag as velocity
            Vector3 lift = new Vector3(0, ((Fl / mass) - g) * dt, 0); //lift as velocity
            Vector3 magnus = Fm * new Vector3(velocity.normalized.z, 0, -velocity.normalized.x) * dt / mass; // Fm * cross product between (0, 1, 0) and velocity magnus as velocity

            this.velocity = this.velocity + drag + lift + magnus;
            transform.position = transform.position + this.velocity * dt;

            transform.RotateAround(this.spin, this.spin.magnitude * dt);
        }
	}
}

