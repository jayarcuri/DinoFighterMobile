using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	MultiSceneMessenger matchInfo;
	
	void Start(){
		matchInfo = GameObject.Find ("MatchInfo").GetComponent<MultiSceneMessenger>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void play() {
		Application.LoadLevel("PlayMenu");
	}
	
	public void settings() {
		Application.LoadLevel("Settings");
	}
	
	public void continueGame() {
//		MultiplayerController.Instance.ShowSelectUI();
	}
	
	public void aiPlay() {
		matchInfo.matchType = GameType.ai;
		Application.LoadLevel("DinoFighter2");
	}
	
	public void localPlay() {
		matchInfo.matchType = GameType.local;
		Application.LoadLevel("DinoFighter2");
	}
	
	
}
