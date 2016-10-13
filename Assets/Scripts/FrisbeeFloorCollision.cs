using UnityEngine;
using System.Collections;

public class FrisbeeFloorCollision : MonoBehaviour {

    private Vector3 e_p = new Vector3(0, 1, 0);

    private const float e = 0.28f;

    Frisbee frisbee;
	// Use this for initialization

    void Start () {
        frisbee = FindObjectOfType<Frisbee>();//get the frisbee object
    }
	
	// Update is called once per frame
	void Update () {
	    if (frisbee.transform.position.y <= 0)
        {
            //Collision happened
            frisbee.transform.position = new Vector3(frisbee.transform.position.x, 0.01f, frisbee.transform.position.z);

            Vector3 e_n = new Vector3(frisbee.transform.position.x, 0, frisbee.transform.position.z);//?

            float V_p = Vector3.Dot(frisbee.velocity, e_p);
            float U_p = -e * V_p;
            float deltaVU_p = U_p - V_p;

            if (frisbee.velocity.y * frisbee.velocity.y > 0.5)
            {
                frisbee.velocity += deltaVU_p * e_p;
            }
            else
            {
                frisbee.velocity = new Vector3(0, 0, 0);
            }
            
        }
	}
}
