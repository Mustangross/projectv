using UnityEngine;
using System.Collections;

public class BaseGameObject : MonoBehaviour
{
	public float f_Health = 0;
	public float f_MaxHealth = 0;
	public bool b_IsJumping = false;
	public bool b_Grounded = false;
	public Vector2 v2_LastCheckPoint;	// this is the spawn position for most objects

	protected virtual void _init()
	{
	}

	public float health
	{
		get { return f_Health; }
		set
		{
			f_Health = Mathf.Clamp (value, 0, f_MaxHealth);
		}
	}

	public float max_health
	{
		get { return f_MaxHealth; }
		protected set
		{
			if (0 < value)
			{
				f_MaxHealth = value;
			}
		}
	}

	public bool jumping
	{
		get { return b_IsJumping; }
		protected set
		{
			b_IsJumping = value;
		}
	}

	public bool grounded
	{
		get { return b_Grounded; }
		set
		{
			b_Grounded = value;
		}
	}

	public Vector2 check_point
	{
		get { return v2_LastCheckPoint; }
		set
		{
			v2_LastCheckPoint = value;
		}
	}
}
