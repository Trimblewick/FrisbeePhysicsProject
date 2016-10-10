using UnityEngine;
using System.Collections;

public class Frisbee : MonoBehaviour {
    public Vector3 velocity;//velocity and also direction
    public Vector3 rotationTest;
    public float radius;
    public float frameRotation;

    private const float gravetyConstant = 9.82f;
    private const float airDensity = 1.23f; //average airdensity at sealevel (kg/m^3)

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        transform.position = this.transform.position + this.velocity * dt;// + new Vector3(0, -gravetyConstant * dt * dt * 0.5f, 0);//position with gravety only
        Quaternion eulerTest = Quaternion.Euler(this.transform.rotation.eulerAngles + (this.rotationTest * dt));
        transform.rotation = eulerTest;
        //transform.RotateAround(new Vector3(1, 0, 1), this.frameRotation * dt);

        //ass
	}
}
