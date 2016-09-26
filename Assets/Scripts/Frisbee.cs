using UnityEngine;
using System.Collections;

public class Frisbee : MonoBehaviour {
    public Vector3 velocity;//velocity and also direction
    

    private const float gravetyConstant = 9.82f;
    
    private int test;
    private float time;
    private Vector3 startPosition; //position at start of the throw

	// Use this for initialization
	void Start () {
        this.test = 0;
        this.time = 0;
        this.startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        time += dt;
        
        transform.position = startPosition + this.velocity * time + new Vector3(0, -gravetyConstant * time * time * 0.5f, 0);//position with gravety only
        
        transform.Rotate(new Vector3(0, 1, 0));//TEST: denna gör rotation med 1 grad per frame
            
	}
}
