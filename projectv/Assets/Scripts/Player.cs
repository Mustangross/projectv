using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float f_Speed = 10f;
	public float f_RunSpeed = 18f;
	public float f_Jump = 10f; 
	public Rigidbody2D rb_RigidBody2D;
	public bool b_Grounded = false;
	private float f_Gravity;


	//Check this line of code in the future.
	//move.rigidbody.velocity = transform.TransformDirection(Vector3.left * f_RunSpeed);

	// Use this for initialization
	void Start () 
	{
		//Getting the RigidBodyComponent 
		rb_RigidBody2D = GetComponent<Rigidbody2D> ();
		//Saving the Original Gravity 
		f_Gravity = rb_RigidBody2D.gravityScale;
	}
	
	// Update is called once per frame
	//void Update () 
	void FixedUpdate ()
	{
		//MOVEMENT
		Movement ();

	}

	//Movement Function
	void  Movement ()
	{
		//TURNING OFF GRAVITY 
		if (b_Grounded == true) {
			rb_RigidBody2D.gravityScale = 0f;
		} else
			rb_RigidBody2D.gravityScale = f_Gravity;

		//MOVING LEFT AND RIGHT
		//Gets Left Arrow Input to move left
		if(Input.GetKey(KeyCode.LeftArrow) == true)
		{
			//moves the Player to the left using the basic speed
			//rb_RigidBody2D.MovePosition(transform.position.x - transform.right * Time.deltaTime * f_Speed);
			transform.position = transform.position - transform.right * Time.deltaTime * f_Speed;
			
			//Ensure the Speed provided is the Run Speed and not the normal speed
			if(Input.GetKey(KeyCode.LeftShift) && b_Grounded == true) // && Input.GetKey(KeyCode.LeftArrow) == true))
			{
				//rb_RigidBody2D.MovePosition (transform.position - transform.right * Time.deltaTime * f_RunSpeed);
				transform.position = transform.position - transform.right * Time.deltaTime * f_RunSpeed;
			}
		}
		
		//Gets Left Arrow Input to move right
		if(Input.GetKey(KeyCode.RightArrow) == true)
		{
			//moves the Player to the right using the basic speed
			//rb_RigidBody2D.MovePosition (transform.position + transform.right * Time.deltaTime * f_Speed);
			transform.position = transform.position + transform.right * Time.deltaTime * f_Speed;
			
			//Ensure the Speed provided is the Run Speed and not the normal speed
			if(Input.GetKey(KeyCode.LeftShift) && b_Grounded == true) // && Input.GetKey(KeyCode.LeftArrow) == true))
			{
				//rb_RigidBody2D.MovePosition (transform.position + transform.right * Time.deltaTime * f_RunSpeed);
				transform.position = transform.position + transform.right * Time.deltaTime * f_RunSpeed;
			}
		}

		//JUMP
		if (Input.GetKey (KeyCode.Space) == true && b_Grounded == true) 
		{
			//rb_RigidBody2D.MovePosition(transform.position + transform.up * Time.deltaTime + new Vector3(0, f_Jump,0));
			rb_RigidBody2D.MovePosition(transform.position + transform.up * Time.deltaTime * f_Jump);

		}
	}
	
	void OnCollisionEnter2D(Collision2D c2D_Collision)
	{
		//Checking if the Player is Grounded 
		if(c2D_Collision.gameObject.tag == "Ground")
		{
			b_Grounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D c2D_Collision)
	{
		//Checking if the Player is Grounded 
		if(c2D_Collision.gameObject.tag == "Ground")
		{
			b_Grounded = false;
		}
	}


}


