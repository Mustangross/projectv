using UnityEngine;
using System.Collections;

public class GoalAchieved : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			Player p_Player;
			//GameObject go_Player = other.gameObject.GetComponent<Player>();
			p_Player = other.gameObject.GetComponent<Player>();
			if(p_Player != null)
			{
				GameObject go_Main = GameObject.FindGameObjectWithTag("MainCamera");
				MainScript ms_Main = go_Main.GetComponent<MainScript>();
				ms_Main.b_Goal1Achieved = true;
			}

		}
	}
}
