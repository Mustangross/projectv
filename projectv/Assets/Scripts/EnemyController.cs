using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public Vector2 spawn_position = new Vector2(5.0f, 1.0f);
	private int i_movement_direction = 1;
	public int i_initial_movement_direction = 1;
	public float f_movement_speed = 0.05f;
	public float f_travel_distance = 5.0f;
	private float f_pause_countdown = 0.0f;
	public float f_pause_interval = 1.0f;
	public bool b_patrolling = true;
	public int i_ai_level = 1;

	public float f_health = 1.0f;

	private Player player;

	// Use this for initialization
	void Start ()
	{
		GameObject player_object = GameObject.FindWithTag ("Player");
		if (null != player_object)
		{
			player = player_object.GetComponent<Player> ();
		}
		else
		{
			Debug.Log("Cannot find Player object!");
		}
		i_movement_direction = i_initial_movement_direction;
		transform.position = spawn_position;
		f_pause_countdown = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (true == b_patrolling)
		{
			Patrol ();
		} 
		else
		{
			Pursue ();
		}

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
			Destroy (gameObject);
		}
	}

	void Patrol()
	{
		if (f_pause_countdown > 0)
		{
			f_pause_countdown -= Time.deltaTime;
		}
		else
		{
			Vector3 pos = transform.position;
			pos.x += i_movement_direction * f_movement_speed;
			transform.position = pos;
			if (pos.x >= spawn_position.x + f_travel_distance || pos.x <= spawn_position.x) {
				i_movement_direction = -i_movement_direction;
				f_pause_countdown = f_pause_interval;
			}
		}
	}

	void Pursue()
	{
		f_pause_countdown = 0;

		if (true == player.b_Grounded)
		{
			// if player's grounded, chase after him
			float pos_diff_sign = Mathf.Sign(player.transform.position.x - transform.position.x);
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
