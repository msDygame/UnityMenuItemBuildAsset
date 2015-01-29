using UnityEngine;
using System.Collections;
	
public class MyScript : MonoBehaviour 
{
	public int MyValue;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		// MyValue should be between 1 and 10 inclusive
		MyValue = Mathf.Clamp(MyValue, 1, 10);
		// do something with MyValue
	}
}
