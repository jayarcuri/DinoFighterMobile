using UnityEngine;
using System.Collections;

public class PlayMenu : MonoBehaviour {

	MultiSceneMessenger matchInfo;

	void Awake () {
//		MultiplayerController.Instance.TrySilentSignIn ();
	}

	void Start(){
		matchInfo = GameObject.Find ("MatchInfo").GetComponent<MultiSceneMessenger>();
	}
	
	public void aiPlay() {
		matchInfo.matchType = GameType.ai;
		Application.LoadLevel("DinoFighter2");
	}
	
	public void quickPlay() {
//		MultiplayerController.Instance.SignInAndStartQuickGame(matchInfo);
	}
	
	public void friendPlay() {
	
	}
	
	public void localPlay() {
		matchInfo.matchType = GameType.local;
		Application.LoadLevel("DinoFighter2");
	}
}
