using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //movement
    public float RUN_SPEED = 8;
    public float WALK_SPEED = 2;


    Rigidbody rigidbody;

    Animator animator;

    private bool facingRight;


    bool grounded = false;

    Collider[] groundCollisions;

    float groundCheckRadius = 0.4f;

    public LayerMask groundLayer;
    public Transform groundCheck;

    public float jumpHeight;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        facingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate() {


        if (grounded && Input.GetAxis("Jump") > 0) {
            grounded = false;
            animator.SetBool("grounded", grounded);
            rigidbody.AddForce(new Vector3(0, jumpHeight, 0));
        }

        //ground collision check
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (groundCollisions.Length > 0)
        {
            grounded = true;
        }
        else {
            grounded = false;
        }

        animator.SetBool("grounded", grounded);

        //lateral movement
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(move));

        float sneaking = Input.GetAxisRaw("Fire3");

        animator.SetFloat("sneaking", sneaking);


        if (sneaking > 0 && grounded)
        {
            rigidbody.velocity = new Vector3(move * WALK_SPEED, rigidbody.velocity.y, 0);
        }
        else {
            rigidbody.velocity = new Vector3(move * RUN_SPEED, rigidbody.velocity.y, 0);
        }
        


        
        //rotation stuff
        if (move > 0 && !facingRight)
        {
            flipCharacter();
            facingRight = true;
        }
        else if (move < 0 && facingRight) {
            flipCharacter();
            facingRight = false;
        }

        

    }

    void flipCharacter() {
        rigidbody.rotation = new Quaternion(rigidbody.rotation.x, rigidbody.rotation.y * -1, rigidbody.rotation.z, rigidbody.rotation.w);
    }
}
