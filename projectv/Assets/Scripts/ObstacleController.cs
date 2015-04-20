using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour
{
	public Vector2 position = new Vector2(-8f, 0.3f);
	public Vector2 dimensions = new Vector2(1f, 0.2f);
	bool is_triggered = false;
	float damage = 1f;

	void Awake()
	{
		transform.localScale = new Vector3(dimensions.x, dimensions.y, 1f);
		transform.position = position;
	}

	void Update()
	{
		// animates
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// TODO: damage player
			// other.gameObject.GetComponent<Player>().
		}
	}

	public void Triggered()
	{
		Debug.Log("Trap triggered!");
	}
}
