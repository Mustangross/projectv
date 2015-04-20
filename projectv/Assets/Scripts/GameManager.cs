using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	int score;

	// Use this for initialization
	void Start ()
	{
		score = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	static public void AddScore(int s)
	{
		GameObject gc_object = GameObject.FindGameObjectWithTag ("GameController");
		if (null != gc_object)
		{
			GameManager gc = gc_object.GetComponent<GameManager> ();
			gc.score += s;

			// update HUD score
			ScoreManager.score = gc.score;
		}
	}
}
