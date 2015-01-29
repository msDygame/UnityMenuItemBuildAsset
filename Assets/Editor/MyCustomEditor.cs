using UnityEngine;
using UnityEditor;
using System.Collections;
//Tips:
//CustomEditor 繼承自 Editor
//MyEditorWindow 繼承自 EditorWindow
//build xxxx.asset 繼承自 ScriptableObject
//客製化 , base is MonoBehaviour type , MyScript.cs
[CustomEditor(typeof(MyScript))]
public class MyCustomEditor : Editor
{
	private Color color ;
	private int iValue = 0 ;
	public override void OnInspectorGUI()
	{
		iValue = EditorGUILayout.IntSlider("ScreenHeight", iValue , 100 , 1920);
		// Only show the damage progress bar if all the objects have the same damage value:
		ProgressBar (iValue / 1920.0f, "Damage");
		EditorGUILayout.LabelField("Separate Line") ;
		color = EditorGUILayout.ColorField("Color" , color ) ;
		if (GUILayout.Button("Press")) {} ;
	}
	// Custom GUILayout progress bar.
	public void ProgressBar (float value ,string label) 
	{
		// Get a rect for the progress bar using the same margins as a textfield:
		Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
		EditorGUI.ProgressBar (rect, value, label);
		EditorGUILayout.Space ();
	}
}

//客製化 , base is ScriptableObject type , Control.cs
[CustomEditor(typeof(Control))]
public class ControlEditor : Editor 
{	
	private Control _target;	
	private SerializedObject mObj;
	private SerializedProperty _text;
	private SerializedProperty _text1;
	private SerializedProperty _text2;
	private SerializedProperty _text3;
	private SerializedProperty _text4;
	private SerializedProperty _text5;
	private SerializedProperty _text6;
	private SerializedProperty _text7;
	private SerializedProperty _text8;
	private SerializedProperty _text9;
	private SerializedProperty _text10;
	private SerializedProperty _text11;
	private SerializedProperty _text12;
	private SerializedProperty _text13;
	private SerializedProperty _text14;
	private SerializedProperty _text15;
	private SerializedProperty _text16;
	private SerializedProperty _text17;
	private SerializedProperty _text18;
	private SerializedProperty _text19;
	private SerializedProperty _text20;
	private SerializedProperty _text21;
	private SerializedProperty _text22;
	private SerializedProperty _text23;
	private SerializedProperty _text24;
	private SerializedProperty _text25;
	private SerializedProperty _text26;
	private SerializedProperty _text27;
	private SerializedProperty _text28;
	private SerializedProperty _text29;
	private SerializedProperty _text30;
	private SerializedProperty _text31;
	private SerializedProperty _text32;
	private SerializedProperty _text33;
	private SerializedProperty _text34;
	private SerializedProperty _IsDebug;	
	void OnEnable()
	{		
		this._target = (Control)base.target;
		this.mObj = new SerializedObject(this._target);
		this._text = this.mObj.FindProperty("text");
		this._text1 = this.mObj.FindProperty("text1");
		this._text2 = this.mObj.FindProperty("text2");
		this._text3 = this.mObj.FindProperty("text3");
		this._text4 = this.mObj.FindProperty("text4");
		this._text5 = this.mObj.FindProperty("text5");
		this._text6 = this.mObj.FindProperty("text6");
		this._text7 = this.mObj.FindProperty("text7");
		this._text8 = this.mObj.FindProperty("text8");
		this._text9 = this.mObj.FindProperty("text9");
		this._text10 = this.mObj.FindProperty("text10");
		this._text11 = this.mObj.FindProperty("text11");
		this._text12 = this.mObj.FindProperty("text12");
		this._text13 = this.mObj.FindProperty("text13");
		this._text14 = this.mObj.FindProperty("text14");
		this._text15 = this.mObj.FindProperty("text15");
		this._text16 = this.mObj.FindProperty("text16");
		this._text17 = this.mObj.FindProperty("text17");
		this._text18 = this.mObj.FindProperty("text18");
		this._text19 = this.mObj.FindProperty("text19");
		this._text20 = this.mObj.FindProperty("text20");
		this._text21 = this.mObj.FindProperty("text21");
		this._text22 = this.mObj.FindProperty("text22");
		this._text23 = this.mObj.FindProperty("text23");
		this._text24 = this.mObj.FindProperty("text24");
		this._text25 = this.mObj.FindProperty("text25");
		this._text26 = this.mObj.FindProperty("text26");
		this._text27 = this.mObj.FindProperty("text27");
		this._text28 = this.mObj.FindProperty("text28");
		this._text29 = this.mObj.FindProperty("text29");
		this._text30 = this.mObj.FindProperty("text30");
		this._text31 = this.mObj.FindProperty("text31");
		this._text32 = this.mObj.FindProperty("text32");
		this._text33 = this.mObj.FindProperty("text33");
		this._text34 = this.mObj.FindProperty("text34");
		this._IsDebug = this.mObj.FindProperty("IsDebug");
	}
	
	public override void OnInspectorGUI ()
	{		
		this.mObj.Update();
		GUILayout.Label("Debug的設定");
		EditorGUILayout.Toggle("啟動DEBUG",this._IsDebug.boolValue);
		EditorGUILayout.PropertyField(this._text26 , new GUIContent("26:固定力道" , "1~5"));
		
		GUILayout.Label("放水AI的設定");
		EditorGUILayout.PropertyField(this._text , new GUIContent("正确揮拍的几率" , "0~1"));
		EditorGUILayout.PropertyField(this._text11 , new GUIContent("11:力道5的機率" , "整數>0"));
		EditorGUILayout.PropertyField(this._text12 , new GUIContent("12:力道4的機率" , "整數>0"));
		EditorGUILayout.PropertyField(this._text13 , new GUIContent("13:力道3的機率" , "整數>0"));
		EditorGUILayout.PropertyField(this._text14 , new GUIContent("14:力道2的機率" , "整數>0"));
		EditorGUILayout.PropertyField(this._text15 , new GUIContent("15:力道1的機率" , "整數>0"));
		
		GUILayout.Label("力道影響設定");
		GUILayout.Label("力道5設定");
		EditorGUILayout.PropertyField(this._text1 , new GUIContent("01:力道5飛行中的球速" , "越大速度越快0~10"));
		EditorGUILayout.PropertyField(this._text2 , new GUIContent("02:力道5的後半斷球速" , "越大速度越快0~1"));
		EditorGUILayout.PropertyField(this._text16 , new GUIContent("16:力道5落點z" , "越大越遠0~6.5"));
		EditorGUILayout.PropertyField(this._text21 , new GUIContent("21:力道5高度上限" , "越大越高>1"));
		
		GUILayout.Label("力道4設定");
		EditorGUILayout.PropertyField(this._text3 , new GUIContent("03:力道4飛行中的球速" , "越大速度越快0~10"));
		EditorGUILayout.PropertyField(this._text4 , new GUIContent("04:力道4的後半斷球速" , "越大速度越快0~1"));
		EditorGUILayout.PropertyField(this._text17 , new GUIContent("17:力道4落點z" , "越大越遠0~6.5"));
		EditorGUILayout.PropertyField(this._text22 , new GUIContent("22:力道4高度上限" , "越大越高>1"));
		
		GUILayout.Label("力道3設定");
		EditorGUILayout.PropertyField(this._text5 , new GUIContent("03:力道3飛行中的球速" , "越大速度越快0~10"));
		EditorGUILayout.PropertyField(this._text6 , new GUIContent("04:力道3的後半斷球速" , "越大速度越快0~1"));
		EditorGUILayout.PropertyField(this._text18 , new GUIContent("18:力道3落點z" , "越大越遠0~6.5"));
		EditorGUILayout.PropertyField(this._text23 , new GUIContent("23:力道3高度上限" , "越大越高>1"));
		
		GUILayout.Label("力道2設定");
		EditorGUILayout.PropertyField(this._text7 , new GUIContent("07:力道2飛行中的球速" , "越大速度越快0~10"));
		EditorGUILayout.PropertyField(this._text8 , new GUIContent("08:力道2的後半斷球速" , "越大速度越快0~1"));
		EditorGUILayout.PropertyField(this._text19 , new GUIContent("19:力道2落點z" , "越大越遠0~6.5"));
		EditorGUILayout.PropertyField(this._text24 , new GUIContent("24:力道2高度上限" , "越大越高>1"));
		
		GUILayout.Label("力道1設定");
		EditorGUILayout.PropertyField(this._text9 , new GUIContent("09:力道1飛行中的球速" , "越大速度越快0~10"));
		EditorGUILayout.PropertyField(this._text10 , new GUIContent("10:力道1的後半斷球速" , "越大速度越快0~1"));
		EditorGUILayout.PropertyField(this._text20 , new GUIContent("20:力道1落點z" , "越大越遠0~6.5"));
		EditorGUILayout.PropertyField(this._text25 , new GUIContent("25:力道1高度上限" , "越大越高>1"));

		GUILayout.Label("難度的設定");
		EditorGUILayout.PropertyField(this._text27 , new GUIContent("27:玩家挥拍的有效范围值" , ">0.0"));
		EditorGUILayout.PropertyField(this._text28 , new GUIContent("28:普通等級的球速減少值" , ">0.0"));
		EditorGUILayout.PropertyField(this._text29 , new GUIContent("29:困難等級的球速減少值" , ">0.0"));

		GUILayout.Label("玩家後方CAMERA的設定");
		EditorGUILayout.PropertyField(this._text30 , new GUIContent("30:Camera 高度" , ">2.0"));
		EditorGUILayout.PropertyField(this._text31 , new GUIContent("31:Camera 深度" , ">-3.2"));
		EditorGUILayout.PropertyField(this._text32 , new GUIContent("32:Camera 追球比例" , ">=0.3, <=1.0"));
		EditorGUILayout.PropertyField(this._text33 , new GUIContent("33:Camera 追球速度" , ">=2.0"));

		GUILayout.Label("小白人的設定");
		EditorGUILayout.PropertyField(this._text34 , new GUIContent("34:lose幾次球要出現小白人" , ">0"));
		this.mObj.ApplyModifiedProperties();
	}
}

//客製化 , base is EditorWindow type , MyEditorWindow.cs
[CustomEditor(typeof(MyEditorWindow))]
public class CustomEditorWindows : Editor 
{	

}