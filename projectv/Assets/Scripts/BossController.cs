using UnityEngine;
using System.Collections;

public class BossController : BaseEnemyObject
{
	// Use this for initialization
	void Start ()
	{
		_init ();
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
		trigger_enter_2D (other);
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
