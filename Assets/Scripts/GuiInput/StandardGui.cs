using UnityEngine;
using System.Collections;
enum EnumDirection : int
{
	Up = 0,
	Down = 1,
	Left = 2,
	Right = 3//
}
//
public enum ConnectStatus{
	
	Preloaded,
	None,
	Complete = 100,
	AccountError = 101,
	PasswordError = 102,
	VerificationCodeError = 103,
	AccountAlreadyExists = 104,
	SameIpSignUpTooMany = 105,
	HttpParameterError = 106,
	SignInconsistent = 107,
	SystemError = 108,
	AccountDisabled = 109,
	IpDisabled = 110,
	AccountFormatError = 111,
	OrderNumberError = 112,
	LackOfBalance = 113,
	OrderProcessed = 114,
	TCLMemberValidationError = 115,
	ReceiveSMSError = 116,
	GameMessageError = 117,
	MemberTokenFail = 118,
	OrderCreationFailed = 119,
	ResetPasswordExcess = 120,
	NoNewData = 121,
	ImsiInfoError = 122,
	LobbyTokenTimeout = 124,
	OemError = 135
}
//
public class StandardGui : MonoBehaviour
{
	public ConnectStatus StatusSpinner ;
	public string upText;
	public Rect upPosition;
	public string downText;
	public Rect downPosition;
	public string leftText;
	public Rect leftPosition;
	public string rightText;
	public Rect rightPosition;
		
	public string AText;
	public Rect APosition;
	public string BText;
	public Rect BPosition;
	public string XText;
	public Rect XPosition;

	public ScreenAspect aspect;
	public int depth;
	public GUIStyle style;	
	private Vector2 scale = Vector2.one;
	//
	void Awake()
	{
		#if UNITY_EDITOR
//		this.aspect = (ScreenAspect)ScriptableObject.Instantiate(this.aspect);
		#endif
	}
	void Start () 
	{
		style = new GUIStyle() ;
		if(this.aspect) this.scale = this.aspect.GUIScale;
	}
	void Update() 
	{
	}
	void OnGUI()
	{
		#if UNITY_EDITOR
		if(this.aspect) this.scale = this.aspect.GUIScale;
		#endif
		GUIUtility.ScaleAroundPivot(this.scale , Vector2.zero);
		GUI.depth = this.depth;
		Show() ;	
	}
	public void Show ()
	{	
		GUI.depth = this.depth;
		if(GUI.Button(this.upPosition , this.upText )) Control((int)EnumDirection.Up);
		else if(GUI.Button(this.downPosition , this.downText )) Control((int)EnumDirection.Down);
		else if(GUI.Button(this.leftPosition , this.leftText )) Control((int)EnumDirection.Left);
		else if(GUI.Button(this.rightPosition , this.rightText )) Control((int)EnumDirection.Right);
		else if(GUI.Button(this.APosition , this.AText )) NextView();
		else if(GUI.Button(this.BPosition , this.BText )) PreviousView();
		else if(GUI.Button(this.XPosition , this.XText )) Exec();
	}
	public void Control(int iDirection)
	{
		if(iDirection == (int)EnumDirection.Up) {}
		else if(iDirection == (int)EnumDirection.Down)  {}
		else if(iDirection == (int)EnumDirection.Left)  {}
		else if(iDirection == (int)EnumDirection.Right) {}
	}
	public void NextView()
	{

	}
	public void PreviousView()
	{
		
	}
	public void Exec()
	{
		
	}
}