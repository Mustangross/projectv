using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public Vector3 spawn_position;
	private int movement_direction;
	public int initial_movement_direction;
	public float movement_speed;
	public float travel_distance;

	// Use this for initialization
	void Start ()
	{
		movement_direction = initial_movement_direction;
		transform.position = spawn_position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 pos = transform.position;
		pos.x += movement_direction * movement_speed;
		transform.position = pos;
		if (pos.x >= spawn_position.x + travel_distance || pos.x <= spawn_position.x)
		{
			movement_direction = -movement_direction;
		}
	}
}
