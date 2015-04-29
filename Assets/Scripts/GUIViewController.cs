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
	Vector2 NavButtonOffscreen;

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
		MatchHistoryOverlay.anchoredPosition = 
		MHO_Homepoint = new Vector2 (-MatchHistoryOverlay.rect.width, 0);

		//no slide UI stuff
		MSO_Gameobj.SetActive (false);
		MHO_Gameobj.SetActive (false);
		NavButtonOffscreen = new Vector2(0, -NavButtonFrame.rect.height*1.35f);
		
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


 