using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float f_Speed = 10f;
	public float f_RunSpeed = 15f;
	public Vector3 teleportPoint;
	public Rigidbody2D rb_RigidBody2D;

	//Check this line of code in the future.
	//move.rigidbody.velocity = transform.TransformDirection(Vector3.left * f_RunSpeed);

	// Use this for initialization
	void Start () 
	{
		rb_RigidBody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Gets Left Arrow Input to move left
		if(Input.GetKey(KeyCode.LeftArrow) == true)
		{
			//moves the Player to the left using the basic speed
			rb_RigidBody2D.MovePosition(transform.position - transform.right * Time.deltaTime * f_Speed);

			//Ensure the Speed provided is the Run Speed and not the normal speed
			if(Input.GetKey(KeyCode.LeftShift)) // && Input.GetKey(KeyCode.LeftArrow) == true))
			{
				rb_RigidBody2D.MovePosition (transform.position - transform.right * Time.deltaTime * f_RunSpeed);
			}
		}

		//Gets Left Arrow Input to move right
		if(Input.GetKey(KeyCode.RightArrow) == true)
		{
			//moves the Player to the right using the basic speed
			rb_RigidBody2D.MovePosition (transform.position + transform.right * Time.deltaTime * f_Speed);
			
			//Ensure the Speed provided is the Run Speed and not the normal speed
			if(Input.GetKey(KeyCode.LeftShift)) // && Input.GetKey(KeyCode.LeftArrow) == true))
			{
				rb_RigidBody2D.MovePosition (transform.position + transform.right * Time.deltaTime * f_RunSpeed);
			}
		}
	}
}
