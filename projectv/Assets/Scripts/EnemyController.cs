using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public Vector3 spawn_position;
	private int movement_direction;
	public int initial_movement_direction;
	public float movement_speed;
	public float travel_distance;
	private float pause_countdown;
	public float pause_interval;

	public GameObject player;

	// Use this for initialization
	void Start ()
	{
		movement_direction = initial_movement_direction;
		transform.position = spawn_position;
		pause_countdown = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (pause_countdown > 0)
		{
			pause_countdown -= Time.deltaTime;
		}
		else
		{
			Vector3 pos = transform.position;
			pos.x += movement_direction * movement_speed;
			transform.position = pos;
			if (pos.x >= spawn_position.x + travel_distance || pos.x <= spawn_position.x) {
				movement_direction = -movement_direction;
				pause_countdown = pause_interval;
			}
		}

		Debug.Log("Distance from player: " + Mathf.Abs(transform.position.x - player.transform.position.x));
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Destroy (gameObject);
		}
	}
}
