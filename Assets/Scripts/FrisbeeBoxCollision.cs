using UnityEngine;
using System.Collections;

public class FrisbeeBoxCollision : MonoBehaviour {
    

    private Frisbee frisbee;

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
        absLengths[0] = Mathf.Abs(lengths[1]);
        absLengths[0] = Mathf.Abs(lengths[2]);
        

        if (absLengths[0] < this.transform.localScale.z * 0.5f && absLengths[1] < this.transform.localScale.x * 0.5f && absLengths[2] < this.transform.localScale.y * 0.5f)
        {
            Vector3 collisionNormal;
            if (absLengths[0] > absLengths[1] && absLengths[0] > absLengths[2])
                collisionNormal = lengths[0] > 0 ? this.transform.forward : -this.transform.forward;
            else if (absLengths[1] > absLengths[0] && absLengths[1] > absLengths[2])
                collisionNormal = lengths[1] > 0 ? this.transform.right : -this.transform.right;
            else
                collisionNormal = lengths[2] > 0 ? this.transform.up : -this.transform.up;
            //frisbee.velocity.z = 0;// frisbee.velocity.magnitude * collisionNormal;
            Debug.Log(collisionNormal);
            Debug.Log("kekeke");
        }

        

	}
}
