using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    private Frisbee frisbee;

    public TextMesh goalText;

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
                    //Goal achieved
                    goalText.text = "GOAL!";
                }
            }
        }
    }
}
