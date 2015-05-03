using UnityEngine;
using System.Collections;

public class GearController : MonoBehaviour
{
	public float f_time_to_live = 3.0f;
	bool activated = false;
	bool grounded = false;
	private GameObject collided_object = null;

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
				if (null != collided_object)
				{
					BaseGameObject a = collided_object.GetComponent<BaseGameObject>();
					a.grounded = false;
				}
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
		else
		{
			// determines if colliding object falls on top
			bool is_on_top = true;
			foreach (ContactPoint2D p in other.contacts)
			{
				Debug.Log(p.normal);
				if (-1f != p.normal.y)
				{
					is_on_top = false;
					break;
				}
			}
			if (true == is_on_top)
			{
				collided_object = other.gameObject;
			}
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		collided_object = null;
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
