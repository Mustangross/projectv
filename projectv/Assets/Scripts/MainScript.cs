using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour
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
			// Check if the Player died and reset his position to the previous checkpoint
		if (p_Player.b_IsPlayerDead)
		{
			p_Player.transform.position = p_Player.v3_LastCheckPoint;
			p_Player.b_IsPlayerDead = false;
		}
	}

	void OnGUI()
	{

	}
}
