using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour
{
	public Player p_Player;
	public float i_StartTimer = 0f;
	public float i_Timer;
	public bool b_Goal1Achieved;

	// Use this for initialization
	void Start ()
	{
		i_StartTimer = Time.time;
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
		i_Timer = Time.time - i_StartTimer;
		if (b_Goal1Achieved)
			Application.LoadLevel ("GameWon");
		if(p_Player.i_Lives < 0)
			Application.LoadLevel ("EndGame");
		// Check if the Player died and reset his position to the previous checkpoint
		if (p_Player.b_IsPlayerDead)
		{
			p_Player.transform.position = p_Player.v3_LastCheckPoint;
			p_Player.b_IsPlayerDead = false;
			p_Player.i_Lives--;
		}


	}

	void OnGUI()
	{
		GUI.TextField(new Rect(100, 100, 150, 20), "LIFE: " +p_Player.i_Lives, 25);
		GUI.TextField(new Rect(200, 100, 150, 20), "TIMER: " +i_Timer, 25);
	}
}
