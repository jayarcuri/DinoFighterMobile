using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;

public class GUIViewController : MonoBehaviour
{
	public Button super;
	public Button burst;
	public RectTransform NavButtonFrame;
	private Vector2 initialTouchLocation;
	Vector2 previousTouchLocation;
	Vector2 TouchEndedAt;

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

	public void SetView(Character character){
		burst.interactable = character.HasBurst ();
		super.interactable = (character.GetMeter () > 9);
	}

	void Awake(){
		MoveSelectionOverlay = MSO_Gameobj.GetComponent<RectTransform> ();
		MatchHistoryOverlay = MHO_Gameobj.GetComponent<RectTransform> ();
		MultiplayerController.Instance.TrySilentSignIn();
	}

	void Start(){
		//MSO_Gameobj.SetActive (true);
		//MHO_Gameobj.SetActive (true);
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
		NavButtonOffscreen = new Vector2(0, -NavButtonFrame.rect.height);
		
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
		print ("Coroute started");
		float startTime = Time.time;

		while (Time.time < 0.25f+startTime) {
			MoveSelectionOverlay.anchoredPosition = Vector2.Lerp(RightSideAnchor, LeftSideAnchor, (Time.time-startTime)/.25f);
			NavButtonFrame.anchoredPosition = Vector2.Lerp (NavButtonHome, NavButtonOffscreen, (Time.time-startTime)/.25f);
			yield return null;
		}
		MoveSelectionOverlay.anchoredPosition = LeftSideAnchor;
		NavButtonFrame.anchoredPosition = NavButtonOffscreen;
	}

	IEnumerator LerpInMainFromMoves(){
		float startTime = Time.time;
		
		while (Time.time < 0.25f+startTime) {
			MoveSelectionOverlay.anchoredPosition = Vector2.Lerp(LeftSideAnchor, RightSideAnchor, (Time.time-startTime)/.25f);
			NavButtonFrame.anchoredPosition = Vector2.Lerp ( NavButtonOffscreen, NavButtonHome, (Time.time-startTime)/.25f);
			yield return null;
		}
		MoveSelectionOverlay.anchoredPosition = RightSideAnchor;
		NavButtonFrame.anchoredPosition = NavButtonHome;
		MSO_Gameobj.SetActive (false);
	}

	IEnumerator LerpInLastClash(){
		if (Screen.width != RightSideAnchor.x)
			RightSideAnchor = new Vector2 (Screen.width, 0);
		float startTime = Time.time;
		MHO_Gameobj.SetActive (true);
		while (Time.time < 0.25f+startTime) {
			MatchHistoryOverlay.anchoredPosition = Vector2.Lerp(MHO_Homepoint, Vector2.zero, (Time.time-startTime)/.25f);
			NavButtonFrame.anchoredPosition = Vector2.Lerp (NavButtonHome, NavButtonOffscreen, (Time.time-startTime)/.25f);
			yield return null;
		}
		MoveSelectionOverlay.anchoredPosition = Vector2.zero;
		NavButtonFrame.anchoredPosition = NavButtonOffscreen;
	}

	IEnumerator LerpInMainFromHistory(){
		float startTime = Time.time;
		
		while (Time.time < 0.25f+startTime) {
			MatchHistoryOverlay.anchoredPosition = Vector2.Lerp(Vector2.zero, MHO_Homepoint, (Time.time-startTime)/.25f);
			NavButtonFrame.anchoredPosition = Vector2.Lerp (NavButtonOffscreen, NavButtonHome, (Time.time-startTime)/.25f);
			yield return null;
		}
		MoveSelectionOverlay.anchoredPosition = MHO_Homepoint;
		NavButtonFrame.anchoredPosition = NavButtonHome;
		MHO_Gameobj.SetActive (false);
	}

	IEnumerator LerpHistoryFromMoves(){
		if (Screen.width != RightSideAnchor.x)
			RightSideAnchor = new Vector2 (Screen.width, 0);
		float startTime = Time.time;
		MHO_Gameobj.SetActive (true);
		while (Time.time < 0.25f+startTime) {
			MoveSelectionOverlay.anchoredPosition = Vector2.Lerp(LeftSideAnchor, RightSideAnchor, (Time.time-startTime)/.25f);
			MatchHistoryOverlay.anchoredPosition = Vector2.Lerp(MHO_Homepoint, Vector2.zero, (Time.time-startTime)/.25f);
			yield return null;
		}
		MoveSelectionOverlay.anchoredPosition = RightSideAnchor;
		MSO_Gameobj.SetActive (false);
		MatchHistoryOverlay.anchoredPosition = Vector2.zero;
	}



}


 