using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


    public float speed;
    private Vector3 movementDirection;

    public Camera playerCamera;
    public Animator anim;

	// Use this for initialization
	void Start () {
        this.playerCamera = FindObjectOfType<Camera>();
        this.anim = GetComponent<Animator>();
        this.movementDirection = transform.forward;
        
	}
	
	// Update is called once per frame
	void Update () {
        

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float cameraRotationScalar = Input.GetAxis("Mouse X");

        this.movementDirection = new Vector3(horizontal, 0, vertical);

        Vector3 movementPosTransform = new Vector3(0, 0, 0);

        if (movementDirection.magnitude > 0.7)
        {
            this.anim.Play("Run");
            movementPosTransform = movementDirection.normalized * speed * 7 * Time.deltaTime;
            this.transform.position += movementPosTransform;
            this.playerCamera.transform.position += movementPosTransform;
            this.transform.forward = this.movementDirection.normalized;
        }
        else if(movementDirection.magnitude > 0.1 && movementDirection.magnitude < 0.7)
        {
            this.anim.Play("Walk");
            movementPosTransform = movementDirection.normalized * speed * Time.deltaTime;
            this.transform.position += movementPosTransform;
            this.playerCamera.transform.position += movementPosTransform;
            this.transform.forward = this.movementDirection.normalized;
        }
        else
        {
            this.anim.Play("Idle");
        }
        
        if (cameraRotationScalar * cameraRotationScalar > 0.003)
        {
            this.playerCamera.transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y + 1.3f, this.transform.position.z));
            
            this.playerCamera.transform.RotateAround(this.transform.position, new Vector3(0, 1, 0), 30 * cameraRotationScalar);
        }

        if (Input.GetKeyDown("1"))
        {
            this.anim.Play("Hit");

        }
        if (Input.GetKeyDown("2"))
        {
            this.anim.Play("Wary");

        }
        if (Input.GetKeyDown("3"))
        {
            this.anim.Play("Walk");

        }
        if (Input.GetKeyDown("4"))
        {
            this.anim.Play("FightStance");

        }
    }
    
	
}
