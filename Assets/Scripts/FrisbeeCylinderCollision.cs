using UnityEngine;
using System.Collections;

public class FrisbeeCylinderCollision : MonoBehaviour {
    private Frisbee frisbee;
    
    public float radius;
    public float stemHeight;
    private const float e = 1.0f;//collision coefficient
    private Vector3 e_n;//normalized direction of friction
    private const float my = 0.1f;//friction coefficient

	// Use this for initialization
	void Start () {
        frisbee = FindObjectOfType<Frisbee>();//get the frisbee object
        
        
	}
	
    float Dot(Vector3 v1, Vector3 v2)
    {
        return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
    }

    Vector3 Cross(Vector3 v1, Vector3 v2)
    {
        Vector3 result = new Vector3(0, 0, 0);

        result.x = v1.y * v2.z - v1.z * v2.y;
        result.y = v1.z * v2.x - v1.x * v2.z;
        result.z = v1.x * v2.y - v1.y * v2.x;

        return result;
    }

	// Update is called once per frame
	void Update () {
        //line of action, wont be affected by y component, is not normalized for e_rååå
        Vector3 lineOfAction = new Vector3(this.transform.position.x, 0, this.transform.position.z) -
                               new Vector3(frisbee.transform.position.x, 0, frisbee.transform.position.z);
        

        if (lineOfAction.magnitude < this.radius + frisbee.getRadius() * Mathf.Cos(frisbee.AoA)/* cos frisbee angle of attack*/)
        {
            if (frisbee.transform.position.y + frisbee.getRadius() * Mathf.Sin(0)/* * sinus of frisbee.angleOfAttack*/ > 0.02)//provided the tree local origen.y is == 0
            {
                if (frisbee.transform.position.y + frisbee.getRadius() * Mathf.Sin(0)/* * sinus of frisbee.angleOfAttack*/< this.transform.position.y + stemHeight)
                {
                    //Object and frisbee have collided
                    //frisbee.transform.position = this.transform.position + lineOfAction.normalized * (this.radius + frisbee.radius + 0.3f);
                    
                    this.e_n = Vector3.Cross(lineOfAction.normalized, frisbee.spin.normalized).normalized;//friction direction
                    float V_n = Vector3.Dot(frisbee.velocity, this.e_n) - frisbee.spin.magnitude * frisbee.getRadius() * 0.5f;//(2 * (Vector3.Dot(frisbee.velocity, this.e_n)) + frisbee.getRadius() * frisbee.spin.magnitude / 360) / 3;
                    Debug.Log(e_n);
                    Debug.Log(Vector3.Dot(frisbee.velocity, this.e_n));
                    Debug.Log(frisbee.getRadius() * frisbee.spin.y);
                    Debug.Log(V_n);

                    frisbee.spin = (V_n / frisbee.getRadius()) * frisbee.spin.normalized;

                    float V_p = Vector3.Dot(frisbee.velocity, lineOfAction.normalized);//Caluculate velocity before collision in line of action v_rååå
                    float U_p = -e * V_p;//Velocity after collision in line of action
                    float deltaVU_p = U_p - V_p;

                    frisbee.velocity += deltaVU_p * lineOfAction.normalized + V_n * e_n;// + deltaVU_p * this.e_n * my;
                    //Rullvillkor uppfyllt?
                    /*if (V_n.magnitude < frisbee.spin.magnitude * frisbee.radius)
                    {

                    }
                    else
                    {
                        //adjusted post collision velocity (utan rullvillkor)
                        
                    }*/
                    //moment of interia for a rotating disk is m*r*r*0.25


                    //float deltaV_p = (1 + e) * V_p;
                    



                    //frisbee.rotationTest = 4 * rotationLineOfAction * deltaV_p * 4;//4 * Mathf.Sin(0)*deltaV_p * 4;
                }
            }
        }

        
	}
}
