using UnityEngine;
using System.Collections;

public class FrisbeeCylinderCollision : MonoBehaviour {
    private Frisbee frisbee;
    
    public float radius;
    public float stemHeight;
    private const float e = 1.0f;//collision coefficient
    private Vector3 e_n = new Vector3(0, 1, 0);//normalized direction of friction
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
        float U_p = 0;//u_rååå aka veclocity in line of action after collision

        if (lineOfAction.magnitude < this.radius + frisbee.radius * Mathf.Cos(0)/* cos frisbee angle of attack*/)
        {
            if (frisbee.transform.position.y + frisbee.radius * Mathf.Sin(0)/* * sinus of frisbee.angleOfAttack*/ > 0.05)//provided the tree local origen.y is == 0
            {
                if (frisbee.transform.position.y + frisbee.radius * Mathf.Sin(0)/* * sinus of frisbee.angleOfAttack*/< this.transform.position.y + stemHeight)
                {
                    //Object and frisbee have collided
                    float V_p = Dot(frisbee.velocity, lineOfAction.normalized);//Caluculate velocity before collision in line of action v_rååå

                    U_p = -e * V_p;//Velocity after collision in line of action

                    float deltaVU_p = U_p - V_p;
                    this.e_n *= my;
                    frisbee.velocity += deltaVU_p * lineOfAction.normalized;// + deltaVU_p * this.e_n;

                    //moment of interia for a rotating disk is m*r*r*0.25
                    

                    float deltaV_p = (1 + e) * V_p;

                    Vector3 rotationLineOfAction = Cross(new Vector3(0, 1, 0), new Vector3(1, 0, 0));



                    frisbee.rotationTest = 4 * rotationLineOfAction * deltaV_p * 4;//4 * Mathf.Sin(0)*deltaV_p * 4;
                }
            }
        }

        
	}
}
