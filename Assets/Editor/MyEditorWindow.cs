using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;//for Dictionary
using System.ComponentModel;//for enum Description
using System.Reflection ;//for enum FieldInfo , DescriptionAttribute
public enum OPTIONS 
{ 
	CUBE = 0, 
	SPHERE = 1, 
	PLANE = 2
}
public enum ENUM_OEM_LIST
{ 
	[Description("000")]
	白牌_000 = 0 ,
	[Description("028")]
	TCL_028 = 1 ,
	[Description("029")]
	海信_029 = 2 ,
	[Description("033")]
	康佳_033 = 3 ,
	[Description("042")]
	長虹_042 = 4 ,
	[Description("043")]
	創維_043 = 5 ,
	[Description("430")]
	創維512M_430 = 6,
	[Description("048")]
	樂視_048 = 7 ,
	[Description("051")]
	海爾_051 = 8 ,
	[Description("520")]
	網訊_520 = 9 ,
	[Description("055")]
	Galpad_055 = 10,
	[Description("056")]
	優酷_056 = 11 ,
	[Description("900")]
	第三方_900 = 12
}
//
public class MyEditorWindow : EditorWindow
{
	//Unity tech sample:
	protected string myString = "Hello World";
	bool groupEnabled;
	protected bool myBool = true;
	float myFloat = 1.23f;
	// Add menu named "My Window" to the Window menu
	[MenuItem("Custom/PropEditorWindow")]
	public static void PropEditorWindowClick()
	{
		// Get existing open window or if none, make a new one:
//		MyEditorWindow window = EditorWindow.GetWindow(MyEditorWindow, true, "My Empty Window");//官方範例,windowtype,Set this to true, to create a floating utility window, false to create a normal window. , title
//		MyEditorWindow window = (MyEditorWindow)EditorWindow.GetWindow (typeof (MyEditorWindow));//title變tab windows
		MyEditorWindow window = EditorWindow.GetWindow<MyEditorWindow>(true);//title is defautl windows format ;
		window.title = "popup Editor Settings Window";
		window.minSize = new Vector2(200 , 960);
	}
	//Dygame settings
	protected SerializedObject mObj;
	protected Dictionary<string , SerializedProperty> properties = new Dictionary<string, SerializedProperty>();
	public string bundleVersion;
	public int bundleVersionCode;
	public string scriptingDefineSymbols;
	private const string DEFSYM_DYGAME_DEBUGMODE = "DYGAME_DEBUGMODE";
	private const string DEFSYM_ZHIWEIDEBUG = "ZhiweiDebug";
	private const string DEFSYM_LOBBY_2_5 = "LOBBY_2_5";
	private const string DEFSYM_LOBBY_2_5_MOON = "LOBBY_2_5_MOON";
	public bool defSym_DYGAME_DEBUGMODE;
	public bool defSym_ZhiweiDebug;
	public bool defSym_LOBBY_2_5;
	public bool defSym_LOBBY_2_5_MOON;
	public Texture2D[] icons;
	protected OPTIONS op = OPTIONS.PLANE ;
	protected int index = 1 ;
	protected float fMin = 1.5f ;
	protected float fMax = 4.5f ;
	protected int iMaskIndex = 1 ; 
	protected bool bState = true ;
	protected string sStateMsg = "" ;
	//test oem+versionCode group
	protected bool bShowOemVersionCode = true ;
	protected ENUM_OEM_LIST[] enumOem  ;
	protected bool[] IsReleaseVersion ;//systemPath
	protected bool[] IsDebugVersion ;//TestPath
	protected string[] sOemCodeString ;//OemCode
	protected int[]  iVersionCode ;
	protected const int ENUM_OEM_LIST_MAX = 13 ;//ENUM_OEM_LIST
	protected string sKeystoreFile = "" ;
	//
	void Awake()
	{
		IsReleaseVersion = new bool[ENUM_OEM_LIST_MAX] ;
		IsDebugVersion = new bool[ENUM_OEM_LIST_MAX] ;
		sOemCodeString = new string[ENUM_OEM_LIST_MAX] ;
		for (int i = 0 ; i < ENUM_OEM_LIST_MAX ; i++) sOemCodeString[i] = "" ;		  
		iVersionCode = new int[ENUM_OEM_LIST_MAX] ;
		enumOem = new ENUM_OEM_LIST[ENUM_OEM_LIST_MAX] ;
		//chinese is ok?
		enumOem[0] = ENUM_OEM_LIST.白牌_000 ;
		enumOem[1] = ENUM_OEM_LIST.TCL_028 ;
		enumOem[2] = ENUM_OEM_LIST.海信_029 ;
		enumOem[3] = ENUM_OEM_LIST.康佳_033 ;
		enumOem[4] = ENUM_OEM_LIST.長虹_042 ;
		enumOem[5] = ENUM_OEM_LIST.創維_043 ;
		enumOem[6] = ENUM_OEM_LIST.創維512M_430 ;
		enumOem[7] = ENUM_OEM_LIST.樂視_048 ;
		enumOem[8] = ENUM_OEM_LIST.海爾_051 ;
		enumOem[9] = ENUM_OEM_LIST.網訊_520 ;
		enumOem[10]= ENUM_OEM_LIST.Galpad_055;
		enumOem[11] = ENUM_OEM_LIST.優酷_056  ;
		enumOem[12] = ENUM_OEM_LIST.第三方_900 ;
		//default VersionCode @ 20140813
		iVersionCode[0] = 28 ;//000
		iVersionCode[1] = 27 ;//TCL
		iVersionCode[2] = 29 ;//029
		iVersionCode[3] = 28 ;//033
		iVersionCode[4] = 1003 ;//042
		iVersionCode[5] = 31 ;//043
		iVersionCode[6] = 31 ;//430
		iVersionCode[7] = 30 ;//048
		iVersionCode[8] = 1000 ;//051
		iVersionCode[9] = 29 ;//520
		iVersionCode[10]= 26 ;//055
		iVersionCode[11]= 27 ;//056
		iVersionCode[12]= 28 ;//900
	}
	// Use this for initialization
	void Start () 
	{
	
	}	
	// Update is called once per frame
	void Update () 
	{
		if(Application.isPlaying) this.Close();
	}
	void OnGUI()
	{
		this.mObj.Update();

		//Select Icon
		GUILayout.BeginHorizontal() ;//水平
		this.properties["icons"].GetArrayElementAtIndex(0).objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Default Icon") , this.properties["icons"].GetArrayElementAtIndex(0).objectReferenceValue , typeof(Texture2D));
		GUILayout.FlexibleSpace() ;//加空
		GUILayout.EndHorizontal() ;
		//Text Filed
		GUILayout.BeginVertical("box");
		EditorGUILayout.PropertyField(this.properties["bundleVersion"] , new GUIContent("Bundle Version*" , "設定 Android 平台的 Version Name"));
		EditorGUILayout.PropertyField(this.properties["bundleVersionCode"] , new GUIContent("Bundle Version Code" , "設定 Android 平台的 Version Code"));
		EditorGUILayout.PropertyField(this.properties["defSym_ZhiweiDebug"], new GUIContent("開發測試GUI" , "顯示『開發期』模擬特殊功能的測試用 GUI 介面"));
		GUILayout.EndVertical();
		//Text Filed + box
		GUILayout.BeginVertical("button");//("box");
		EditorGUILayout.PropertyField(this.properties["defSym_DYGAME_DEBUGMODE"], new GUIContent("測試版" , "在畫面左上角顯示測試訊息及 Web API 路徑"));
		EditorGUILayout.PropertyField(this.properties["defSym_LOBBY_2_5"], new GUIContent("2.5版功能" , "指定輸出包含2.5版功能及場景資源"));
		EditorGUILayout.PropertyField(this.properties["defSym_LOBBY_2_5_MOON"], new GUIContent("2.5版功能 月亮版" , "指定輸出包含2.5版功能及場景資源(含月亮主題特效的版本"));
		GUILayout.EndVertical();
		//button
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("確定"))
		{ 	
			PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown , this.icons); 
			PlayerSettings.bundleVersion = this.bundleVersion;
			PlayerSettings.Android.bundleVersionCode = this.bundleVersionCode;
			EditorUserBuildSettings.development = true;
			ShowNotification(new GUIContent("設定完成"));
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		//Text Field
		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField ("Text Field", myString);	
		//toggle settings
		groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
		myBool = EditorGUILayout.Toggle ("Toggle", myBool);//checkbox
		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);//slider
		EditorGUILayout.EndToggleGroup ();
		//Multi Text Field
		EditorGUILayout.TextArea("TextArea") ;
		//int Field
		EditorGUILayout.IntField(1) ;
		EditorGUILayout.LabelField("Min Val:", fMin.ToString());
		EditorGUILayout.LabelField("Max Val:", fMax.ToString());
		//Min-Max Slider
		EditorGUILayout.MinMaxSlider(ref fMin,ref fMax,0.0f,100.0f) ;
		EditorGUILayout.LabelField("Popup Index:", index.ToString());
		//combox 
		string[] sOptions = { "Cube", "Sphere", "Plane" } ;
		index = EditorGUILayout.Popup(index, sOptions);
		//Enum combox
		EditorGUILayout.LabelField("EnumPopup Index:", ((int)op).ToString());
		op = (OPTIONS)EditorGUILayout.EnumPopup("EnumPopup:", op );
		//Multi Checkbox
		iMaskIndex = EditorGUILayout.MaskField ("Mask Flags", iMaskIndex, sOptions);
		//Foldout Bar
		bState = EditorGUILayout.Foldout(bState , "Attribate") ;
		if (bState)
		{
			GUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Click Here!");
			GUILayout.FlexibleSpace();
			if(GUILayout.Button("Destory Self!")) { this.Close(); }
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}
		//
		Vector2 p2 = new Vector2(0.0f , 0.0f) ;
		Vector3 p3 = new Vector3(0.0f , 0.0f , 0.0f) ;
		Vector4 p4 = new Vector4(0.0f , 0.0f , 0.0f , 0.0f) ;
		p2 = EditorGUILayout.Vector2Field("Point:", p2);
		p3 = EditorGUILayout.Vector3Field("Point:", p3);
		p4 = EditorGUILayout.Vector4Field("Point:", p4);
		//set OemCode + VersionCode
		bShowOemVersionCode = EditorGUILayout.Foldout(bShowOemVersionCode , "OemCode") ;
		string str = "" ;
		if (bShowOemVersionCode)
		{
			for(int i = 0 ; i < ENUM_OEM_LIST_MAX ; i++)
			{
				GUILayout.BeginHorizontal("box");
				//enum oem
				enumOem[i] = (ENUM_OEM_LIST)EditorGUILayout.EnumPopup("", enumOem[i] );
				//oem int field
				FieldInfo EnumInfo = enumOem[i].GetType().GetField(enumOem[i].ToString());
				DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[]) EnumInfo.GetCustomAttributes (typeof(DescriptionAttribute), false);
				str = "" + EnumAttributes.Length ;
				if (EnumAttributes.Length > 0)
				{
					if (string.IsNullOrEmpty(sOemCodeString[i]) == true) sOemCodeString[i] = string.Copy( EnumAttributes[0].Description ) ;
				}
				sOemCodeString[i] = EditorGUILayout.TextField ("", sOemCodeString[i]);
				//
				iVersionCode[i] = EditorGUILayout.IntField(iVersionCode[i]) ;
				//
				IsReleaseVersion[i] = EditorGUILayout.Toggle ("正式版", IsReleaseVersion[i]);
				IsDebugVersion[i] = EditorGUILayout.Toggle ("測試版", IsDebugVersion[i]);
				GUILayout.EndHorizontal();
			}
			//keystore file location
			GUILayout.BeginHorizontal("box");
			if(GUILayout.Button("Browse Keystore"))
			{ 	
				sKeystoreFile = EditorUtility.OpenFilePanel("Browse Keystore" , "" , "keystore");
			}
			GUILayout.Label (sKeystoreFile, EditorStyles.boldLabel);
			GUILayout.EndHorizontal();
			//build apk
			if(GUILayout.Button("產生APK"))
			{ 	
				bool b = (EditorUtility.DisplayDialog("Infomation" , " build APK?" , "YES", "cancel")) ;
				if (b == false) return ;//cancel
				//keystore is null
				if (string.IsNullOrEmpty(sKeystoreFile) == true) 
				{
					EditorUtility.DisplayDialog("Keystore?","Keystore is NULL" , "QUIT", "") ;
					return ;
				}
				//copy keystore and aliasKey
				PlayerSettings.Android.keystoreName = sKeystoreFile ;//依人而異
				PlayerSettings.Android.keystorePass = "aibelive!2345" ;//上面的keystore的密碼
				PlayerSettings.Android.keyaliasName = "aiwi_key" ;//固定,依附aiwi.keystore
				PlayerSettings.Android.keyaliasPass = "aibelive!2345" ;//上面的keyalias的密碼
				//build apk file
				int iTotalFiles = 0 ; 
				int iCountFiles = 0 ;
				for (int i = 0 ; i < ENUM_OEM_LIST_MAX ; i++)
				{
					if (IsReleaseVersion[i]) iTotalFiles++ ;
					if (IsDebugVersion[i]) iTotalFiles++ ;
				}
				//Ex:Dygame_3.0.1024_49_121221_1_T_B2_051.apk
				string sProjectName = "Dygame_" ;//Dygame_
				string sOriginalBundle = string.Copy(PlayerSettings.bundleVersion) ;//Ex:"3.0.1024_49_121221_1"
				string sVersionName = "" ;//3.0.1024
				string sVersionCode = "" ;//_49_, this need be replace , one apk per version code
				string sDateString = "" ;//121221
				string sBuildCount = "" ;//_1_
				int    iBuildCount = 1 ;//
				string sReleaseVersion = "_R_B2_" ;//B1=Android 4.4.2 SDK 22.3 ApiLevel 19 Unity4.3.3
				string sDebugVersion = "_T_B2_" ;//B2=Android 4.1.2 SDK 20.0.3 ApiLevel 16 Unity4.1.3
				string sBuildVersion = "" ;//use sDebugVersion or sReleaseVersion
				string sAPKFilename = "" ;
				bool  IsDebugVersionApk = false ;
				//split token
				bool  IsErrorHappen = false ;
				string[] sToken = sOriginalBundle.Split("_"[0]) ;
				if (string.IsNullOrEmpty(sToken[0]) == false) sVersionName = string.Copy(sToken[0]) ;
				else IsErrorHappen = true ;
				if (string.IsNullOrEmpty(sToken[1]) == true)  IsErrorHappen = true ;//oem
				if (string.IsNullOrEmpty(sToken[2]) == false) sDateString = string.Copy(sToken[2]) ;
				else IsErrorHappen = true ;
				if (string.IsNullOrEmpty(sToken[3]) == false) { sBuildCount = string.Copy(sToken[3]) ; int.TryParse(sBuildCount, out iBuildCount); }
				else IsErrorHappen = true ;
				if (IsErrorHappen == true)
				{
					EditorUtility.DisplayDialog("Bundle Version error?","Bundle Version format is Error. Ex: 3.0.1024_49_121221_1" , "QUIT", "") ;
				}
				//build debug.apk then release.apk
				if (IsErrorHappen == false)
				{
					for (int j = 0 ; j < 2 ; j++)//debug then release 
					{
						//build test path
						if (j == 0)	
						{ 
							sBuildVersion = sDebugVersion ; 
							IsDebugVersionApk = true ; 
						}
						//build release path
						else 
						{ 
							sBuildVersion = sReleaseVersion ; 
							IsDebugVersionApk = false ; 
						}
						//build apk
						for (int i = 0 ; i < ENUM_OEM_LIST_MAX ; i++)
						{
							//skip false(未勾選)
							if ((IsDebugVersionApk) && (IsDebugVersion[i] == false)) continue ;
							else if ((!IsDebugVersionApk) && (IsReleaseVersion[i] == false)) continue ;
							//replaced VersionCode 
							sVersionCode = "_" + iVersionCode[i] + "_" ;
							//incoming BuildCount 
							if (iBuildCount <= 0) iBuildCount = 1 ;//min from 1
							sBuildCount = "_" + iBuildCount + "_" ;
							//target file name
							sAPKFilename = sProjectName + sVersionName + sVersionCode + sDateString + sBuildCount + sBuildVersion + sOemCodeString[i] + ".apk" ;
							EditorUtility.DisplayProgressBar("Auto Build "+iCountFiles+" - "+iTotalFiles , sAPKFilename , (float)(iCountFiles)/iTotalFiles )  ;
						//	BuildPipeline.BuildPlayer(sSelectedScene , sAPKFilename , BuildTarget.Android ,opt);
							EditorUtility.DisplayDialog("Test?","This is create a apk.." , "Yes", "") ;
							//
							iCountFiles++ ;
							iBuildCount++ ;
						}
					}
				}
				//
				ShowNotification(new GUIContent("完成"));
				EditorUtility.ClearProgressBar() ;
			}
		}		
		//
		this.mObj.ApplyModifiedProperties();	

		base.Repaint();
	}
	public void OnInspectorGUI () 
	{
		string[] sOptions = { "Cube", "Sphere", "Plane" } ;
		index = EditorGUILayout.Popup(index, sOptions);
		op = (OPTIONS)EditorGUILayout.EnumPopup("EnumPopup:", op);
		EditorGUILayout.MinMaxSlider(ref fMin,ref fMax,0.0f,100.0f) ;
	}
	void OnEnable()
	{		
		this.icons = PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.Unknown);

		this.bundleVersion = PlayerSettings.bundleVersion;
		this.bundleVersionCode = PlayerSettings.Android.bundleVersionCode;
		this.scriptingDefineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
		
		this.defSym_DYGAME_DEBUGMODE = this.scriptingDefineSymbols.IndexOf(DEFSYM_DYGAME_DEBUGMODE) >= 0;
		this.defSym_ZhiweiDebug = this.scriptingDefineSymbols.IndexOf(DEFSYM_ZHIWEIDEBUG) >= 0;
		this.defSym_LOBBY_2_5 = this.scriptingDefineSymbols.IndexOf(DEFSYM_LOBBY_2_5) >= 0;
		this.defSym_LOBBY_2_5_MOON = this.scriptingDefineSymbols.IndexOf(DEFSYM_LOBBY_2_5_MOON) >= 0;

		this.mObj = new SerializedObject(this);
		
		this.AddProperty("bundleVersion");
		this.AddProperty("bundleVersionCode");
		this.AddProperty("developmentBuild");
		this.AddProperty("defSym_DYGAME_DEBUGMODE");
		this.AddProperty("defSym_ZhiweiDebug");
		this.AddProperty("defSym_LOBBY_2_5");
		this.AddProperty("defSym_LOBBY_2_5_MOON");
		this.AddProperty("icons");
	}
	protected void AddProperty(string fieldName)
	{
		this.properties.Add(fieldName , this.mObj.FindProperty(fieldName));
	}
	//
	private static void OnScene(SceneView sceneview)
	{
		EditorUtility.DisplayDialog("cpopup Editor Window", "OnSceneUI", "OK", "");	
		Debug.Log("This event opens up so many possibilities.");
	}
    void OnHierarchyChange()
	{
		EditorUtility.DisplayDialog("cpopup Editor Window", "OnHierarchyChange", "OK", "");
		Debug.Log("This event opens up so many possibilities.");
	}
	void InstantiatePrimitive(OPTIONS op) 
	{
		switch (op) 
		{
		case OPTIONS.CUBE:	break;
		case OPTIONS.SPHERE:break;
		case OPTIONS.PLANE: break;
		default: Debug.LogError("Unrecognized Option");	break;
		}
	}
}
