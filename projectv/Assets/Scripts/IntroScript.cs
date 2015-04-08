using UnityEngine;
using System.Collections;

public class IntroScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		Debug.Log ("screen width: " +Screen.width);
		Debug.Log ("screen width: " +Screen.height);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnGUI()
	{	
		//Find a fucking way to control the camera
		if (GUI.Button (new Rect (500, 100, 200, 200), "Start"))
			Application.LoadLevel ("Main");
	}
}
