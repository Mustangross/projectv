using UnityEngine;
using System.Collections;

public class BaseEnemyObject : BaseGameObject
{
	protected int i_movement_direction = 1;
	public int i_initial_movement_direction = 1;
	public float f_movement_speed = 0.05f;
	public float f_travel_distance = 5.0f;
	public int i_ai_level = 1;
	public int i_worth = 10;

	protected GameObject _player_object;

	protected override void _init ()
	{
		base._init ();
		_player_object = GameObject.FindGameObjectWithTag ("Player");
		if (null == _player_object)
		{
			Debug.Log("Cannot find Player object!");
		}
		i_movement_direction = i_initial_movement_direction;
		transform.position = check_point;
	}

	protected virtual void trigger_enter_2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Vector3 collision_point = other.bounds.ClosestPoint(transform.position);
			float stomp_test = Vector3.Dot(Vector3.up, (collision_point - transform.position).normalized);
			Debug.Log (stomp_test);

			Player p_Player = player_object.GetComponent<Player> ();
			if(p_Player != null)
			{
				if(0.6f > stomp_test)
				{
					p_Player.health--;
				}
			}

			if (0.6f <= stomp_test)
			{
				f_Health -= 1f;
				if (0 >= f_Health)
				{
					// update score
					GameManager.AddScore(worth);
					Destroy (gameObject, 0.01f);
				}
			}
		}
	}

	public int movement_direction
	{
		get { return i_movement_direction; }
		protected set
		{
			i_movement_direction = value;
		}
	}

	public float movement_speed
	{
		get { return f_movement_speed; }
		protected set
		{
			if (0 <= value)
			{
				f_movement_speed = value;
			}
		}
	}

	public int ai_level
	{
		get { return i_ai_level; }
		protected set
		{
			i_ai_level = value;
		}
	}

	public int worth
	{
		get { return i_worth; }
		protected set
		{
			i_worth = value;
		}
	}

	public GameObject player_object
	{
		get { return _player_object; }
		protected set
		{
			_player_object = value;
		}
	}
}
