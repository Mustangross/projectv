using UnityEngine;
using System.Collections;

public class CheckPoints : MonoBehaviour
{
	public Player p_Player;
	// Use this for initialization
	void Start ()
	{
		//Remenber how to find a gameobject using a script
		GameObject go_Player = GameObject.FindGameObjectWithTag ("Player");
		if (go_Player != null)
		{
			p_Player = go_Player.GetComponent<Player> ();
		}
		else
		{
			Debug.Log("Somebody deleted the Player o.O");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		//Checking if the Player is Grounded 
		if (other.gameObject.tag == "Player")
		{

			Vector3 v3_CheckPointPosition = transform.position;
			p_Player.v3_LastCheckPoint = v3_CheckPointPosition;
			Destroy(gameObject);
		}
		
	}
}