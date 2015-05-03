using UnityEngine;
using System.Collections;

public class CheckPoints : MonoBehaviour
{
	void OnTriggerEnter2D (Collider2D other)
	{
		//Checking if the Player is Grounded 
		if (other.gameObject.tag == "Player")
		{
			Player p_Player;
			p_Player = other.gameObject.GetComponent<Player> ();
			if(p_Player != null)
			{
				Vector3 v3_CheckPointPosition = transform.position;
				p_Player.check_point = v3_CheckPointPosition;
				Destroy(gameObject);
			}
		}
		
	}
}