using UnityEngine;
using System.Collections;

public class TheCamera : MonoBehaviour 
{
	public int i_Deadzone = 5;
	public Vector3 v3_InitialCameraPosition;
	public Player p_Player;
	
	// Use this for initialization
	void Start () 
	{
		GameObject go_Player = GameObject.FindGameObjectWithTag ("Player");

		if (go_Player != null) {
			p_Player = go_Player.GetComponent<Player> ();
		} else {
			Debug.Log ("Somebody deleted the Player o.O");
		}

		//	v3_CameraOffsetx = transform.position.x - p_Player.transform.position + new Vector3 (10,0,0);

		v3_InitialCameraPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		Debug.Log (p_Player.transform.position.x - v3_InitialCameraPosition.x);
		if ((p_Player.transform.position.x - transform.position.x) > i_Deadzone) 
		{
			//v3_InitialCameraOffset = transform.position - p_Player.transform.position;
			transform.position = v3_InitialCameraPosition + new Vector3 (p_Player.transform.position.x,0,0) - new Vector3 (i_Deadzone,0,0);
			//transform.position = v3_InitialCameraOffset + new Vector3 (0, f_Playery, 0);
		}

		if ((p_Player.transform.position.x - transform.position.x) <  -i_Deadzone) 
		{
			transform.position = v3_InitialCameraPosition + new Vector3 (p_Player.transform.position.x,0,0) + new Vector3 (i_Deadzone,0,0);
				//transform.position = v3_InitialCameraOffset + new Vector3 (0, f_Playery, 0);
		}
	}
}