using UnityEngine;
using System.Collections;

public class PlayMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void Awake () {
		MultiplayerController.Instance.TrySilentSignIn();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void quickPlay() {
		MultiplayerController.Instance.SignInAndStartMPGame();
		Application.LoadLevel("DinoFighter2");
	}
	
	public void friendPlay() {
	
	}
}
