using UnityEngine;
using System.Collections;

public class ScreenAspect : ScriptableObject 
{
	public float width;
	public float height;
		
	public float Ratio
	{
		get
		{
			return this.width / height;
		}
	}
		
	public Vector2 GUIScale
	{
		get
		{
			return new Vector2((float)Screen.width / this.width , (float)Screen.height / this.height);
		}
	}
}