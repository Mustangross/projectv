using UnityEngine;
using System.Collections;

public class CollectibleController : MonoBehaviour
{
	public Vector2 position;
	public int worth = 10;

	// Use this for initialization
	void Awake ()
	{
		transform.position = position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (new Vector3 (0, 0, 45) * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// update score
			GameManager.AddScore(worth);
		}
	}

	public void Collected()
	{
		Debug.Log("Item collected!");
	}
}
