using UnityEngine;
using System.Collections;

public class GearController : MonoBehaviour
{
	public float f_time_to_live = 3.0f;
	bool activated = false;
	bool grounded = false;

	// Use this for initialization
	void Awake ()
	{
		activated = false;
		grounded = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (activated && grounded)
		{
			f_time_to_live -= Time.deltaTime;
			if (0 >= f_time_to_live)
			{
				Destroy(gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if("Ground" == other.gameObject.tag)
		{
			grounded = true;
			GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}

	public void Activate()
	{
		activated = true;
		GetComponent<Rigidbody2D>().isKinematic = false;
		GetComponent<BoxCollider2D>().isTrigger = false;
		transform.parent = null;
		Vector3 pos = transform.position;
		pos.z = 0;
		transform.position = pos;
	}
}
