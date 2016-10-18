using UnityEngine;
using System.Collections;

public class FrisbeeFloorCollision : MonoBehaviour {

    private Vector3 e_p = new Vector3(0, 1, 0);

    private const float e = 0.32f;
    private const float my = 0.02f;//friction coefficient

    public LineRenderer lineRenderer;
    private bool drawLine;

    public Frisbee frisbee;
	// Use this for initialization

    void Start () {
        frisbee = FindObjectOfType<Frisbee>();//get the frisbee object
        this.drawLine = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (frisbee.transform.position.y <= 0)
        {
            //Collision happened
            frisbee.transform.position = new Vector3(frisbee.transform.position.x, 0.01f, frisbee.transform.position.z);

            Vector3 e_n = new Vector3(frisbee.velocity.x, 0, frisbee.velocity.z).normalized;

            float V_p = Vector3.Dot(frisbee.velocity, e_p);
            float U_p = -e * V_p;
            float deltaVU_p = U_p - V_p;

            

            if (frisbee.velocity.y * frisbee.velocity.y > 0.3f)
            {
                frisbee.velocity += deltaVU_p * e_p + deltaVU_p * my * e_n;
                
            }
            else
            {
                frisbee.velocity = new Vector3(0, 0, 0);
                frisbee.spin = new Vector3(0, 0, 0);
                frisbee.transform.rotation = new Quaternion(0,0,0,0);
                frisbee.idle = true;
                this.drawLine = true;
            }
            
        }
        else if (frisbee.transform.position.y > 0.2f)
        {
            this.drawLine = false;
        }
        if (drawLine)
        {
            this.lineRenderer.SetPosition(0, new Vector3(frisbee.transform.position.x, 25, frisbee.transform.position.z));
            this.lineRenderer.SetPosition(1, frisbee.transform.position);
        }
        else
        {
            this.lineRenderer.SetPosition(0, new Vector3(-10, -10, -10));
            this.lineRenderer.SetPosition(1, new Vector3(-10, -10, -10));
        }
	}
}
