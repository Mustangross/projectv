using UnityEngine;
using System.Collections;

public class WonGame : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnGUI()
	{
		 GUI.TextField(new Rect(500, 100, 150, 20), "CONGRATULATIONS", 25);
		//Find a fucking way to control the camera
		if (GUI.Button (new Rect (500, 120, 60, 20), "Restart"))
			Application.LoadLevel ("Main");
		if (GUI.Button (new Rect (500, 140, 60, 20), "Quit"))
			Application.Quit ();
	}
}
