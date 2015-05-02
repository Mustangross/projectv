using UnityEngine;
using System.Collections;

public class BossController : BaseGameObject
{
	private int i_movement_direction = 1;
	public float f_movement_speed = 0.05f;
	public float f_travel_distance = 5.0f;
	public int i_ai_level = 1;

	public int worth = 100;

	private GameObject player_object;

	// Use this for initialization
	void Start ()
	{
		player_object = GameObject.FindGameObjectWithTag ("Player");
		if (null == player_object)
		{
			Debug.Log("Cannot find Player object!");
		}
		transform.position = check_point;
		f_Health = 3.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Pursue ();

		// checks the distance from player
//		if (null != player)
//		{
//			float d1 = Mathf.Abs ((transform.position.x + 0.5f * transform.localScale.x) - (player.transform.position.x - 0.5f * player.transform.localScale.x));
//			float d2 = Mathf.Abs ((transform.position.x - 0.5f * transform.localScale.x) - (player.transform.position.x + 0.5f * player.transform.localScale.x));
//			Debug.Log("Distance from player: " + Mathf.Min(d1, d2));
//		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// if taken damage
			f_Health -= 1f;
			if (0 >= f_Health)
			{
				// update score
				GameManager.AddScore(worth);

				Destroy (gameObject);
			}
		}
	}

	void Pursue()
	{
		Player player = player_object.GetComponent<Player> ();
	
		if (true == player.grounded)
		{
			// if player's grounded, chase after him
			float pos_diff_sign = Mathf.Sign(player_object.transform.position.x - transform.position.x);
			i_movement_direction = pos_diff_sign < 0 ? -1 : 1;
			transform.position += new Vector3(i_movement_direction * f_movement_speed, 0, 0);
		}
		else
		{
			// player is airborne
			switch (i_ai_level)
			{
			case 1:
				{
					// continue forward
					transform.position += new Vector3(i_movement_direction * f_movement_speed, 0, 0);
				}
				break;
			case 2:
				{
					// continue forward at 1/2 speed
					transform.position += new Vector3(0.5f * i_movement_direction * f_movement_speed, 0, 0);
				}
				break;
			case 3:
				{
					// stop
				}
				break;
			case 4:
				{
					// back up at 1/2 speed
					transform.position += new Vector3(-0.5f * i_movement_direction * f_movement_speed, 0, 0);
				}
				break;
			default:
				break;
			}
		}
	}
}
