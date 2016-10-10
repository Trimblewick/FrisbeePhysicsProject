using UnityEngine;
using System.Collections;

public class FrisbeeBoxCollision : MonoBehaviour {

    private Vector3 [] boxCorners = new Vector3[8];

    private Frisbee frisbee;

	// Use this for initialization
	void Start () {
        frisbee = FindObjectOfType<Frisbee>();//get the frisbee object

        boxCorners[0] = new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z - 1);
        boxCorners[1] = new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z + 1);
        boxCorners[2] = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z - 1);
        boxCorners[3] = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z + 1);
        boxCorners[4] = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z - 1);
        boxCorners[5] = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z + 1);
        boxCorners[6] = new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z - 1);
        boxCorners[7] = new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z + 1);

        

    }
	
	// Update is called once per frame
	void Update () {
        bool outsidebox = true;
        for (int i = 0; i < 8 && outsidebox; i++)
        {
           // if ((frisbee.transform.position - boxCorners[0]).magnitude > 1)
            {
                outsidebox = false;
            }
        }
	}
}
