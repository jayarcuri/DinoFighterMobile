using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		MultiplayerController.Instance.TrySilentSignIn();
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
}
