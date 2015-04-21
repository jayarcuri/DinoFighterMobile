using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;

public class GUIViewController : MonoBehaviour
{
	public Button super;
	public Button burst;
	public Text turnText;
	public RectTransform NavButtonFrame;
	private Vector2 initialTouchLocation;
	//Vector2 previousTouchLocation;
	//Vector2 TouchEndedAt;

	public GameObject MSO_Gameobj;
	public GameObject MHO_Gameobj;
	RectTransform MoveSelectionOverlay;
	RectTransform MatchHistoryOverlay;

	Vector2 LeftSideAnchor;		//Following variables used in GUI move animations
	Vector2 RightSideAnchor;
	Vector2 MHO_Homepoint;
	Vector2 StartPoint;
	Vector2 NavButtonHome;
	Vector2 MiddlePoint;
	Vector2 NavButtonOffscreen;
	float animationStart;
	float journeyLength;

	public void SetView(Character character, int turn){
		burst.interactable = character.GetMeter() > 4;
		super.interactable = (character.GetMeter () > 9);
		turnText.text = "Player " + turn + "'s turn!";
	}

	void Awake(){
		MoveSelectionOverlay = MSO_Gameobj.GetComponent<RectTransform> ();
		MatchHistoryOverlay = MHO_Gameobj.GetComponent<RectTransform> ();
		MultiplayerController.Instance.TrySilentSignIn();
	}

	void Start(){
		MoveSelectionOverlay.anchoredPosition = new Vector2(Screen.width, 0);
		LeftSideAnchor = new Vector2 (0f, 0);
		RightSideAnchor = new Vector2 (Screen.width, 0);
		NavButtonHome = new Vector2 (NavButtonFrame.anchoredPosition.x, NavButtonFrame.anchoredPosition.y);
		MiddlePoint = new Vector3 (Screen.width / 2, Screen.height / 2);
		MatchHistoryOverlay.anchoredPosition = 
		MHO_Homepoint = new Vector2 (-MatchHistoryOverlay.rect.width, 0);

		//no slide UI stuff
		MSO_Gameobj.SetActive (false);
		MHO_Gameobj.SetActive (false);
		NavButtonOffscreen = new Vector2(0, -NavButtonFrame.rect.height*1.2f);
		
	}

	void Update(){



		/*	if (Input.GetMouseButtonDown (0)) {
			initialTouchLocation = (Vector2)Input.mousePosition;
			previousTouchLocation = (Vector2)Input.mousePosition;
		}

		if (Input.GetMouseButton (0)) {

			if (MoveSelectionOverlay.anchoredPosition.x < RightSideAnchor.x || 
			    (!AreOverlaysOnscreen() && Input.mousePosition.x - previousTouchLocation.x < 0)) {	//Block for controlling MSO when onscreen

				if (Input.mousePosition.x != previousTouchLocation.x) {
					MoveSelectionOverlay.anchoredPosition 
						= new Vector2 (Mathf.Clamp(MoveSelectionOverlay.anchoredPosition.x + (Input.mousePosition.x - previousTouchLocation.x), 0f, RightSideAnchor.x), 0);
		
				}
			}	// Block end


			else if (MatchHistoryOverlay.anchoredPosition.x > MHO_Homepoint.x || 
			    (!AreOverlaysOnscreen() && Input.mousePosition.x - previousTouchLocation.x > 0)) {	//Block for controlling MHO when onscreen
				
				if (MatchHistoryOverlay.anchoredPosition.x - 								// Don't let MHO go
				    (previousTouchLocation.x - Input.mousePosition.x) <= MHO_Homepoint.x){	// past left edge
					MatchHistoryOverlay.anchoredPosition = MHO_Homepoint;}
				
				if (Input.mousePosition.x != previousTouchLocation.x) {
					MatchHistoryOverlay.anchoredPosition 
						= new Vector2 (Mathf.Clamp(MatchHistoryOverlay.anchoredPosition.x - 
							(previousTouchLocation.x - Input.mousePosition.x), MHO_Homepoint.x, 0), 0);
				}
			}	// Block end 
			previousTouchLocation = (Vector2)Input.mousePosition;

		}

		if (Input.GetMouseButtonUp (0)) {
			initialTouchLocation = Vector2.zero;
			previousTouchLocation = Vector2.zero;
			if (MoveSelectionOverlay.anchoredPosition.x < RightSideAnchor.x)
				TouchEndedAt = new Vector2(MoveSelectionOverlay.anchoredPosition.x, 0);
			if(MatchHistoryOverlay.anchoredPosition.x > MHO_Homepoint.x)
				TouchEndedAt = new Vector2(MatchHistoryOverlay.anchoredPosition.x, 0);

			animationStart = Time.time;
		}

		if (!Input.GetMouseButton (0)) {
			if (MoveSelectionOverlay.anchoredPosition.x < RightSideAnchor.x){
				if(MoveSelectionOverlay.anchoredPosition.x < MiddlePoint.x)
					MoveSelectionOverlay.anchoredPosition = Vector2.Lerp(TouchEndedAt, LeftSideAnchor, Mathf.Clamp(((Time.time-animationStart)/.25f), 0, 1));
				if(MoveSelectionOverlay.anchoredPosition.x >= MiddlePoint.x)
					MoveSelectionOverlay.anchoredPosition = Vector2.Lerp(TouchEndedAt, RightSideAnchor, Mathf.Clamp(((Time.time-animationStart)/.25f), 0, 1));
			}

			if (MatchHistoryOverlay.anchoredPosition.x > MHO_Homepoint.x){
				if(MatchHistoryOverlay.anchoredPosition.x > MHO_Homepoint.x/2)
					MatchHistoryOverlay.anchoredPosition = Vector2.Lerp(TouchEndedAt, Vector2.zero, Mathf.Clamp(((Time.time-animationStart)/.25f), 0, 1));
				if(MatchHistoryOverlay.anchoredPosition.x <= MHO_Homepoint.x/2)
					MatchHistoryOverlay.anchoredPosition = Vector2.Lerp(TouchEndedAt, MHO_Homepoint, Mathf.Clamp(((Time.time-animationStart)/.25f), 0, 1));
			}

		}*/



	}
	
	private bool AreOverlaysOnscreen(){
			return (MatchHistoryOverlay.anchoredPosition.x > MHO_Homepoint.x
			        || MoveSelectionOverlay.anchoredPosition.x < RightSideAnchor.x);
	}

	public void SummonMoves(){
		StartCoroutine(LerpInMoves());
	}

	public void SummonMainFromMoves(){
		StartCoroutine (LerpInMainFromMoves ());
	}

	public void SummonHistory(){
		StartCoroutine (LerpInLastClash ());
	}

	public void SummonMainFromHistory(){
		StartCoroutine (LerpInMainFromHistory ());
	}

	public void SummonHistoryFromMoves(){
		StartCoroutine(LerpHistoryFromMoves());
	}

	IEnumerator LerpInMoves(){
		MSO_Gameobj.SetActive (true);
		float startTime = Time.time;

		while (MoveSelectionOverlay.anchoredPosition.x > LeftSideAnchor.x) {
			MoveSelectionOverlay.anchoredPosition = Vector2.Lerp(RightSideAnchor, LeftSideAnchor, (Time.time-startTime)/.25f);
			NavButtonFrame.anchoredPosition = Vector2.Lerp (NavButtonHome, NavButtonOffscreen, (Time.time-startTime)/.25f);
			yield return null;}
	}

	IEnumerator LerpInMainFromMoves(){
		float startTime = Time.time;
		
		while (MoveSelectionOverlay.anchoredPosition.x < RightSideAnchor.x) {
			MoveSelectionOverlay.anchoredPosition = Vector2.Lerp(LeftSideAnchor, RightSideAnchor, (Time.time-startTime)/.25f);
			NavButtonFrame.anchoredPosition = Vector2.Lerp ( NavButtonOffscreen, NavButtonHome, (Time.time-startTime)/.25f);
			yield return null;
		}
		MSO_Gameobj.SetActive (false);
	}

	IEnumerator LerpInLastClash(){
		float startTime = Time.time;
		MHO_Gameobj.SetActive (true);

		while (MatchHistoryOverlay.anchoredPosition.x < 0) {
			MatchHistoryOverlay.anchoredPosition = Vector2.Lerp(MHO_Homepoint, Vector2.zero, Mathf.Clamp((Time.time-startTime)/.25f, 0f, 1f));
			NavButtonFrame.anchoredPosition = Vector2.Lerp (NavButtonHome, NavButtonOffscreen, Mathf.Clamp((Time.time-startTime)/.25f, 0f, 1f));
			yield return null;
		}
	}

	IEnumerator LerpInMainFromHistory(){
		float startTime = Time.time;
		
		while (Time.time < 0.25f+startTime) {
			MatchHistoryOverlay.anchoredPosition = Vector2.Lerp(Vector2.zero, MHO_Homepoint, Mathf.Clamp((Time.time-startTime)/.25f, 0f, 1f));
			NavButtonFrame.anchoredPosition = Vector2.Lerp (NavButtonOffscreen, NavButtonHome, Mathf.Clamp((Time.time-startTime)/.25f, 0f, 1f));
			yield return null;
		}
		MHO_Gameobj.SetActive (false);
	}

	IEnumerator LerpHistoryFromMoves(){
		float startTime = Time.time;
		MHO_Gameobj.SetActive (true);
		while (MatchHistoryOverlay.anchoredPosition.x < 0) {
			MoveSelectionOverlay.anchoredPosition = Vector2.Lerp(LeftSideAnchor, RightSideAnchor, Mathf.Clamp((Time.time-startTime)/.25f, 0f, 1f));
			MatchHistoryOverlay.anchoredPosition = Vector2.Lerp(MHO_Homepoint, Vector2.zero, Mathf.Clamp((Time.time-startTime)/.25f, 0f, 1f));
			yield return null;
		}
		MSO_Gameobj.SetActive (false);
	}



}


 