using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


    public float speed;
    private Vector3 movementDirection;

    public Camera playerCamera;
    public Animator anim;

    private Frisbee frisbee;

    public GameObject hand;
    private bool pickedUpFrisbee;
    
    private Vector3 moveToHellPos = new Vector3(50, -50, 50);


    public LineRenderer lineRenderer;
    private Vector3 endOfLine;
    // Use this for initialization
    void Start () {
        this.playerCamera = FindObjectOfType<Camera>();
        this.anim = GetComponent<Animator>();
        this.movementDirection = transform.forward;
        frisbee = FindObjectOfType<Frisbee>();//get the frisbee object
        pickedUpFrisbee = false;
        this.lineRenderer.SetPosition(0, hand.transform.position);// moveToHellPos;
        this.endOfLine = hand.transform.position;
        this.lineRenderer.SetPosition(1, endOfLine);
        
    }

    // Update is called once per frame
    void Update()
    {
        //frisbee.idle = pickedUpFrisbee;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float cameraRotationScalar = Input.GetAxis("Mouse X");
        float cameraRotationScalar2 = Input.GetAxis("Mouse Y");

        
        if (!pickedUpFrisbee)
        {
            this.movementDirection = vertical * new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z).normalized
                + horizontal * playerCamera.transform.right.normalized;

            Vector3 movementPosTransform = new Vector3(0, 0, 0);

            if (movementDirection.magnitude > 0.81)
            {
                this.anim.Play("Run");
                movementPosTransform = movementDirection.normalized * speed * 6 * Time.deltaTime;
                this.transform.position += movementPosTransform;
                this.playerCamera.transform.position += movementPosTransform;
                this.transform.forward = this.movementDirection.normalized;
            }
            else if (movementDirection.magnitude > 0.1 && movementDirection.magnitude < 0.81)
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
        }
            
        if (cameraRotationScalar * cameraRotationScalar > 0.003)
        {
            this.playerCamera.transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y + 1.3f, this.transform.position.z));

            this.playerCamera.transform.RotateAround(this.transform.position, new Vector3(0, 1, 0), 40 * cameraRotationScalar);
        }
        if (this.pickedUpFrisbee)
        {
            frisbee.transform.position = hand.transform.position;
            this.anim.Play("FightStance");
            
            

            if (Input.GetButton("Aim"))
            {
                //draw aimline
                this.lineRenderer.SetPosition(0, this.hand.transform.position);
                endOfLine += vertical * new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z).normalized * Time.deltaTime
                + horizontal * playerCamera.transform.right.normalized * Time.deltaTime - new Vector3(0, cameraRotationScalar2 * 10.0f, 0) * Time.deltaTime;
                this.lineRenderer.SetPosition(1, endOfLine);

                Vector3 throwDirection = endOfLine - hand.transform.position;

                float powerOfThrow = 10.0f;

                if (Input.GetButtonDown("Fire1"))
                {
                    frisbee.velocity = throwDirection * powerOfThrow;
                    frisbee.spin = new Vector3(0, -20, 0);
                    this.pickedUpFrisbee = false;
                    frisbee.idle = false;
                
                }
            }
            else
            {
                
            }
            if (Input.GetButtonDown("Fire2"))
            {
                //drop frisbee
                frisbee.velocity = new Vector3(0, 0, 0);
                this.pickedUpFrisbee = false;
                frisbee.idle = false;
            }
        }
        else
        {
            //reset aimline
            this.lineRenderer.SetPosition(0, this.hand.transform.position);
            this.endOfLine = this.hand.transform.position;
            this.lineRenderer.SetPosition(1, endOfLine);

            if (Input.GetButtonDown("Fire2"))
            {
                float playerDistanceToFrisbee = (frisbee.transform.position - this.transform.position).magnitude;
                if (playerDistanceToFrisbee < 1.5f)
                {
                    frisbee.spin = new Vector3(0, 0, 0);
                    this.pickedUpFrisbee = true;
                    
                }
                else
                {
                    //to far away from frisbee
                }
            }
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
