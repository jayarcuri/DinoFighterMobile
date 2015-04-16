using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		MultiplayerController.Instance.TrySignIn();
//		MultiplayerController.Instance.TrySilentSignIn();
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
		MultiplayerController.Instance.ShowSelectUI();
	}
	
	
}
