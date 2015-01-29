using UnityEngine;
using System.Collections;
using UnityEditor;
public class MyMenuItem : MonoBehaviour
{

	// Use this for initialization
	void Start () 
    {
	
	}	
	// Update is called once per frame
	void Update () 
    {
	
	}
    //
    [MenuItem("Editor/BuildAsset/create ScreenAsset")]
    public static void BuildScreenAspectAsset()
    {
        ScreenAspect atest = ScriptableObject.CreateInstance<ScreenAspect>();
        AssetDatabase.CreateAsset(atest, "Assets/Settings/ScreenAspect.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("create Asset", "ScreenAspect.asset", "OK", "");
    }
	//
	[MenuItem("Editor/BuildAsset/create ControlAsset")]
	public static void BuildControlAspectAsset()
	{
		Control atest = ScriptableObject.CreateInstance<Control>();//ScriptableObject
		AssetDatabase.CreateAsset(atest, "Assets/Settings/Control.asset");
		AssetDatabase.SaveAssets();
		EditorUtility.DisplayDialog("create Asset", "Control.asset", "OK", "");
	}
}
/*
	//
	[ExecuteInEditMode]
	//
	[AddComponentMenu('111/222')]

	EditorWindow

	EditorApplication

	Help

	ObjectPreview

	Tools
*/	
