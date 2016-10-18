using UnityEngine;
using System.Collections;

public class FrisbeeBoxCollision : MonoBehaviour {
    
    private Frisbee frisbee;
    private const float e = 0.42f;//collision coefficient

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
                    float boxRadius = 0;
                    if (absLengths[0] / this.transform.localScale.z > absLengths[1] / this.transform.localScale.x && absLengths[0] / this.transform.localScale.z > absLengths[2] / this.transform.localScale.y)
                    {
                        lineOfAction = lengths[0] > 0 ? this.transform.forward : -this.transform.forward;
                        boxRadius = this.transform.localScale.z * 0.5f;
                    }
                    else if (absLengths[1] / this.transform.localScale.x  > absLengths[0] / this.transform.localScale.z && absLengths[1] / this.transform.localScale.x > absLengths[2] / this.transform.localScale.y)
                    {
                        lineOfAction = lengths[1] > 0 ? this.transform.right : -this.transform.right;
                        boxRadius = this.transform.localScale.x * 0.5f;
                    }
                    else
                    {
                        lineOfAction = lengths[2] > 0 ? this.transform.up : -this.transform.up;
                        boxRadius = this.transform.localScale.y * 0.5f;
                    }
                    
                    //X is the point you get if you move from the centerpoint of the box, in the direction of lineOfAction, with the sum of the radiuses
                    Vector3 X = this.transform.position + (lineOfAction.normalized * (boxRadius + frisbee.getRadius() + 0.01f));
                    X = new Vector3(X.x, frisbee.transform.position.y, X.z);
                    Vector3 proj = X - frisbee.transform.position;      //proj is the distance from X to the center of the frisbee. This will be projected in the lineOfAction
                    float mag = Vector3.Dot(proj, lineOfAction.normalized);
                    frisbee.transform.position += mag * lineOfAction.normalized; //move the frisbee with the magnitude of proj in lineOfAction, #doyouevenengland

                    e_n = Vector3.Cross(lineOfAction, Vector3.Cross(new Vector3(frisbee.velocity.x, 0, frisbee.velocity.z), lineOfAction)).normalized;


                    float V_p = Vector3.Dot(frisbee.velocity, lineOfAction.normalized);
                    float U_p = -e * V_p;
                    float deltaVU_p = U_p - V_p;

                    float V_n = 0;
                    //kolla om rullvillkoret uppfylls
                    float marginOfError = 1.0f;
                    if (frisbee.spin.magnitude > frisbee.velocity.magnitude / frisbee.getRadius() - marginOfError && frisbee.spin.magnitude < frisbee.velocity.magnitude / frisbee.getRadius() + marginOfError)
                    {
                        V_n = (2 * (Vector3.Dot(frisbee.velocity, e_n)) + frisbee.getRadius() * frisbee.spin.magnitude) / 3;
                        frisbee.spin = new Vector3(0, (Vector3.Dot(frisbee.velocity, e_n) + V_n) / frisbee.getRadius(), 0);
                    }
                    else
                    {
                        V_n = deltaVU_p * my;
                        frisbee.spin += (2 * my * deltaVU_p / frisbee.getRadius()) * Vector3.Cross(-lineOfAction.normalized, e_n);
                    }

                    frisbee.velocity += deltaVU_p * lineOfAction.normalized + V_n * e_n;
                    
                    
                }
            }
        }

        
	}
}
