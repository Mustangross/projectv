using UnityEngine;
using System.Collections;

public class CollectibleController : MonoBehaviour
{
	public Vector2 position;

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

	public void Collected()
	{
		Debug.Log("Item collected!");
	}
}
