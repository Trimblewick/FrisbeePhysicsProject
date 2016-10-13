using UnityEngine;
using System.Collections;

public class FrisbeeBoxCollision : MonoBehaviour {
    
    private Frisbee frisbee;
    private const float e = 1.0f;//collision coefficient

    private const float my = 0.1f;//friction coefficient


    float Dot(Vector3 v1, Vector3 v2)
    {
        return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
    }

    // Use this for initialization
    void Start () {
        frisbee = FindObjectOfType<Frisbee>();//get the frisbee object
        
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 BoxToFrisbee = frisbee.transform.position - this.transform.position;
        float[] lengths = new float[3];
        lengths[0] = Vector3.Dot(this.transform.forward, BoxToFrisbee);
        lengths[1] = Vector3.Dot(this.transform.right, BoxToFrisbee);
        lengths[2] = Vector3.Dot(this.transform.up, BoxToFrisbee);
        float[] absLengths = new float[3];
        absLengths[0] = Mathf.Abs(lengths[0]);
        absLengths[1] = Mathf.Abs(lengths[1]);
        absLengths[2] = Mathf.Abs(lengths[2]);

        
        if (absLengths[0] < this.transform.localScale.z * 0.5f)
        {
            if (absLengths[1] < this.transform.localScale.x * 0.5f)
            {
                if (absLengths[2] < this.transform.localScale.y * 0.5f)
                {
                    Vector3 lineOfAction;
                    Vector3 e_n;//friction direction
                    if (absLengths[0] > absLengths[1] && absLengths[0] > absLengths[2])
                    {
                        lineOfAction = lengths[0] > 0 ? this.transform.forward : -this.transform.forward;

                    }
                    else if (absLengths[1] > absLengths[0] && absLengths[1] > absLengths[2])
                        lineOfAction = lengths[1] > 0 ? this.transform.right : -this.transform.right;
                    else
                        lineOfAction = lengths[2] > 0 ? this.transform.up : -this.transform.up;

                    e_n = Vector3.Cross(lineOfAction, frisbee.spin.normalized);
                    float V_p = Vector3.Dot(frisbee.velocity, lineOfAction.normalized);
                    float U_p = -e * V_p;
                    float deltaVU_p = U_p - V_p;

                    float V_n = Vector3.Dot(frisbee.velocity, e_n) - frisbee.spin.magnitude * frisbee.getRadius() * 0.5f;

                    frisbee.velocity += deltaVU_p * lineOfAction.normalized + V_n * e_n;
                    
                    
                }
            }
        }

        
	}
}
