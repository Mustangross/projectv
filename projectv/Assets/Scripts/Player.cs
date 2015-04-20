using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public int i_Lives = 2;
	public float f_Speed = 10f;
	public float f_MaxRunSpeed = 15f;
	public float f_RunSpeedDecrement = 1f;
	public float f_RunSpeed = 15f;
	public float f_JumpIncreament = 7f;
	public float f_JumpCurvature = 1.01f;
	public float f_Jump = 0f;
	public float f_MaxJump = 40f;
	public Vector3 v3_LastCheckPoint;
	private float f_Gravity;
	public bool b_IsJumping = false;
	public bool b_IsPlayerDead = false;
	public bool b_Grounded = false;
	private Rigidbody2D rb_RigidBody2D;
	
	
	//Check this line of code in the future.
	//move.rigidbody.velocity = transform.TransformDirection(Vector3.left * f_RunSpeed);
	
	// Use this for initialization
	void Start ()
	{
		//Set a Initial Position 
		v3_LastCheckPoint = transform.position;
		//Getting the RigidBodyComponent 
		rb_RigidBody2D = GetComponent<Rigidbody2D> ();
		//Saving the Original Gravity 
		f_Gravity = rb_RigidBody2D.gravityScale;
	}
	
	// Update is called once per frame
	//void Update () 
	void FixedUpdate ()
	{
		//Disables Movement if Player is dead
		if (b_IsPlayerDead == false)
		{
			//MOVEMENT
			Movement ();
		}
		else 
		{
			Debug.Log ("Died");
			//Reduce Player lives
			//DO SOMETHING
			//Ensure the World Resets
			//Ensure Player Goes Back to Latest checkpoint
			//Ensure Sounds Involving the player no longer play
			//Ensure Enemies understand the player is no longer alive
			//Ensure a proper trasition happens between the player dying and world reseting
			//Ensure the World timer resets
			//Ensure the player becomes alive after reseting


		}
	}
	
	//Movement Function
	void  Movement ()
	{
			//TURNING OFF GRAVITY 
			if (b_Grounded == true)
			{
				rb_RigidBody2D.gravityScale = 0f;
			} else
				rb_RigidBody2D.gravityScale = f_Gravity;
			
			//MOVING LEFT AND RIGHT
			//Gets Left Arrow Input to move left
			if (Input.GetKey (KeyCode.LeftArrow) == true)
			{
				//Ensure the Speed provided is the Run Speed and not the normal speed
				if (Input.GetKey (KeyCode.LeftShift) && b_Grounded == true) { // && Input.GetKey(KeyCode.LeftArrow) == true))
					//Slowering when in the air
					if (!b_Grounded && f_RunSpeed > f_Speed)
						f_RunSpeed -= f_RunSpeedDecrement;
					else
						f_RunSpeed = f_MaxRunSpeed;
					//rb_RigidBody2D.MovePosition (transform.position - transform.right * Time.deltaTime * f_RunSpeed);
					transform.position = transform.position - transform.right * Time.deltaTime * f_RunSpeed;
				} 
				else 
				{
					//moves the Player to the left using the basic speed
					//rb_RigidBody2D.MovePosition(transform.position.x - transform.right * Time.deltaTime * f_Speed);
					transform.position = transform.position - transform.right * Time.deltaTime * f_Speed;
				}
			}
			
			//Gets Left Arrow Input to move right
			if (Input.GetKey (KeyCode.RightArrow) == true) 
			{
				//Ensure the Speed provided is the Run Speed and not the normal speed
				if (Input.GetKey (KeyCode.LeftShift) && b_Grounded == true) { // && Input.GetKey(KeyCode.LeftArrow) == true))
					//Slowering when in the air
					if (!b_Grounded && f_RunSpeed > f_Speed)
						f_RunSpeed -= f_RunSpeedDecrement;
					else
						f_RunSpeed = f_MaxRunSpeed;
					//rb_RigidBody2D.MovePosition (transform.position + transform.right * Time.deltaTime * f_RunSpeed);
					transform.position = transform.position + transform.right * Time.deltaTime * f_RunSpeed;
				} 
				else 
				{
					//moves the Player to the right using the basic speed
					//rb_RigidBody2D.MovePosition (transform.position + transform.right * Time.deltaTime * f_Speed);
					transform.position = transform.position + transform.right * Time.deltaTime * f_Speed;
				}
			}
			
			//JUMP
			//Check if the user tap to jump and if the player was grounded
			if (Input.GetKey (KeyCode.Space) == true && b_Grounded == true) 
				b_IsJumping = true;
			
			//Incrementing Jump to make it smoother
			if (f_Jump < f_MaxJump && b_IsJumping) 
			{
				f_Jump += f_JumpIncreament * f_JumpCurvature;
				rb_RigidBody2D.MovePosition (transform.position + transform.up * Time.deltaTime * f_Jump);
			}
			
			//Understands that the user is no longer jumping and resets the jump speed
			if (Input.GetKeyUp (KeyCode.Space) || f_Jump >= f_MaxJump)
			{
				b_IsJumping = false;
				f_Jump = 0f;
			}	
	}
	
	//Check for when the Object enters the collison
	void OnCollisionEnter2D (Collision2D c2D_Collision)
	{
		//Checking if the Player is Grounded 
		if (c2D_Collision.gameObject.tag == "Ground")
		{
			b_Grounded = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			b_IsJumping = true;
		}
		else if (other.gameObject.tag == "Obstacle")
		{
			ObstacleController obstacle_controller = other.gameObject.GetComponent<ObstacleController>();
			obstacle_controller.Triggered();
			// TODO: set dead flag instead of destroying game object here
			Destroy(gameObject);
		}
		else if (other.gameObject.tag == "Collectible")
		{
			CollectibleController collectible_controller = other.gameObject.GetComponent<CollectibleController>();
			collectible_controller.Collected();
			Destroy(other.gameObject);
		}
	}
	
	//Check for when the Object leaves the collison
	void OnCollisionExit2D (Collision2D c2D_Collision)
	{
		//Checking if the Player is Grounded 
		if (c2D_Collision.gameObject.tag == "Ground") 
		{
			b_Grounded = false;
		}
	}
	
	
}

/*
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float f_Speed = 10f;
	public float f_RunSpeed = 18f;
	public float f_Jump = 100f; 
	public Rigidbody2D rb_RigidBody2D;
	public bool b_Grounded = false;
	private float f_Gravity;
	
	public float f_airborne_speed_attenuation = 0.1f;
	public float f_horizontal_jump_force = 50.0f;
	public float f_running_horizontal_jump_multiplier = 1.5f;
	
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
		
		float adjusted_speed = f_Speed;
		float adjusted_run_speed = f_RunSpeed;
		if (false == b_Grounded)
		{
			adjusted_speed *= f_airborne_speed_attenuation;
			adjusted_run_speed *= f_airborne_speed_attenuation;
		}
		
		//MOVING LEFT AND RIGHT
		if(true == Input.GetKey(KeyCode.LeftArrow) || true == Input.GetKey(KeyCode.RightArrow))
		{
			//moves the Player using the basic speed
			Vector3 direction;
			if(true == Input.GetKey(KeyCode.LeftArrow))
			{
				direction = -transform.right;
			}
			else
			{
				direction = transform.right;
			}
			//rb_RigidBody2D.MovePosition(transform.position.x - transform.right * Time.deltaTime * f_Speed);
			transform.position = transform.position + direction * Time.deltaTime * adjusted_speed;
			
			//Ensure the Speed provided is the Run Speed and not the normal speed
			if(Input.GetKey(KeyCode.LeftShift) && b_Grounded == true) // && Input.GetKey(KeyCode.LeftArrow) == true))
			{
				//rb_RigidBody2D.MovePosition (transform.position - transform.right * Time.deltaTime * f_RunSpeed);
				transform.position = transform.position + direction * Time.deltaTime * adjusted_run_speed;
			}
		}
		
		//JUMP
		if (Input.GetKey (KeyCode.Space) == true && b_Grounded == true) 
		{
			//rb_RigidBody2D.MovePosition(transform.position + transform.up * Time.deltaTime + new Vector3(0, f_Jump,0));
			//			rb_RigidBody2D.MovePosition(transform.position + transform.up * Time.deltaTime * f_Jump);
			Vector2 force = new Vector2(0, f_Jump);
			if(true == Input.GetKey(KeyCode.RightArrow))
			{
				force.x = f_horizontal_jump_force;
			}
			else if(true == Input.GetKey(KeyCode.LeftArrow))
			{
				force.x = -f_horizontal_jump_force;
			}
			
			// increase the horizontal force if we're running
			if(true == Input.GetKey(KeyCode.LeftShift))
			{
				force.x *= f_running_horizontal_jump_multiplier;
			}
			rb_RigidBody2D.AddForce(force);
		}
	}
	
	void OnCollisionEnter2D(Collision2D c2D_Collision)
	{
		//Checking if the Player is Grounded 
		if(c2D_Collision.gameObject.tag == "Ground")
		{
			// reset our velocity if we're landing
			if (false == b_Grounded)
			{
				rb_RigidBody2D.velocity = Vector2.zero;
			}
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
	
	
}*/


