using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float f_Speed = 10f;
	public float f_RunSpeed = 15f;
	public float f_NormalSpeed = 10f;
	public float f_RunSpeedIncrement = 1f;
	public float f_RunSpeedDecrement = 0.25f;
	public float f_JumpIncreament = 1f;
	public float f_MaxJump = 100f;
	public float f_Jump = 0f; 
	public Rigidbody2D rb_RigidBody2D;
	public bool b_Grounded = false;
	public bool b_OnAir = false;
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
		if (b_Grounded == true) 
		{
			rb_RigidBody2D.gravityScale = 0f;
		} 
		else 
		{
			rb_RigidBody2D.gravityScale = f_Gravity;
		}
		
		//MOVING LEFT AND RIGHT
		//Gets Left Arrow Input to move left
		if (Input.GetKey (KeyCode.LeftArrow) == true && Input.GetKey (KeyCode.LeftShift) == true && b_Grounded) 
		{
			//Accelerates Player
			if(f_Speed < f_RunSpeed)
			{
				f_Speed += f_RunSpeedIncrement;
			}
			transform.position = transform.position - transform.right * Time.deltaTime * f_Speed;

		} 	//moves the Player to the left using the walking speed
		else if (Input.GetKey (KeyCode.LeftArrow) == true) 
		{
			//Accelerates Player
			if(!b_Grounded && f_Speed > f_NormalSpeed)
			{
				f_Speed -= f_RunSpeedDecrement;
			}
			else if(f_Speed > f_NormalSpeed)
			{
				f_Speed -= f_RunSpeedIncrement;
			}
			transform.position = transform.position - transform.right * Time.deltaTime * f_Speed;
		}

		if (Input.GetKey (KeyCode.RightArrow) == true && Input.GetKey (KeyCode.LeftShift) == true && b_Grounded) 
		{
			//Accelerates Player
			if(f_Speed < f_RunSpeed)
			{
				f_Speed += f_RunSpeedIncrement;
			}
			transform.position = transform.position + transform.right * Time.deltaTime * f_Speed;
			
		} 	//moves the Player to the left using the walking speed
		else if (Input.GetKey (KeyCode.RightArrow) == true) 
		{
			//Accelerates Player
			if(!b_Grounded && f_Speed > f_NormalSpeed)
			{
				f_Speed -= f_RunSpeedDecrement;
			}
			else if(f_Speed > f_NormalSpeed)
			{
				f_Speed -= f_RunSpeedIncrement;
			}
			transform.position = transform.position + transform.right * Time.deltaTime * f_Speed;
		}

		
	/*	//Ensure the Speed isn't accelariting while off the ground
		if (!b_Grounded && f_RunSpeed > f_Speed) 
			f_RunSpeed -= f_RunSpeedDecrement;
		else
			f_RunSpeed = f_MaxRunSpeed;*/
		
		//JUMP
		if (Input.GetKey (KeyCode.Space) == true && b_Grounded == true) 
		{
			b_OnAir = true;
			f_Jump = 0f;
		}
		if (b_OnAir && f_Jump < f_MaxJump)
		{
			f_Jump += f_JumpIncreament;
			//rb_RigidBody2D.MovePosition(transform.position + transform.up * Time.deltaTime + new Vector3(0, f_Jump,0));
			//rb_RigidBody2D.MovePosition(transform.position + transform.up * Time.deltaTime * f_Jump);
			transform.position = transform.position + transform.up * Time.deltaTime * f_Jump;
		} 
			
	}
	
	void OnCollisionEnter2D(Collision2D c2D_Collision)
	{
		//Checking if the Player is Grounded 
		if (c2D_Collision.gameObject.tag == "Ground")
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
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Obstacle")
		{
			ObstacleController obstacle_controller = other.gameObject.GetComponent<ObstacleController>();
			obstacle_controller.Triggered();
			Destroy(gameObject);
		}
		else if (other.gameObject.tag == "Collectible")
		{
			CollectibleController collectible_controller = other.gameObject.GetComponent<CollectibleController>();
			collectible_controller.Collected();
			Destroy(other.gameObject);
		}
	}
}



/*using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float f_Speed = 10f;
	public float f_RunSpeed = 18f;
	public float f_Jump = 100f; 
	public Rigidbody2D rb_RigidBody2D;
	public bool b_Grounded = false;
	private float f_Gravity = 0;

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



