using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;

public class MultiplayerController {

	private static MultiplayerController _instance = null;
	
	const int MinOpponents = 2;
	const int MaxOpponents = 2;
	const int Variant = 0;
	
	private MultiplayerController() {
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate ();
	}
	
	public static MultiplayerController Instance {
		get {
			if (_instance == null) {
				_instance = new MultiplayerController();
			}
			return _instance;
		}
	}
	
	public void SignInAndStartQuickGame() {
		if (! PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.localUser.Authenticate((bool success) => {
				if (success) {
					Debug.Log ("We're signed in! Welcome " + PlayGamesPlatform.Instance.localUser.userName);
					PlayGamesPlatform.Instance.TurnBased.CreateQuickMatch(MinOpponents, MaxOpponents,
					                                                      Variant, OnMatchStarted);
					// We could start our game now
				} else {
					Debug.Log ("Oh... we're not signed in.");
				}
			});
		} else {
			Debug.Log ("You're already signed in.");
			PlayGamesPlatform.Instance.TurnBased.CreateQuickMatch(MinOpponents, MaxOpponents,
			                                                      Variant, OnMatchStarted);
		}
	}
	
	public void TrySignIn() {
		if (! PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.localUser.Authenticate ((bool success) => {
				if (success) {
					Debug.Log ("Signed in! Welcome " + PlayGamesPlatform.Instance.localUser.userName);
				} else {
					Debug.Log ("Oh... we're not signed in.");
				}
			});
		} else {
			Debug.Log("We're already signed in");
		}
	}
	
	public void TrySilentSignIn() {
		if (! PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.Authenticate ((bool success) => {
				if (success) {
					Debug.Log ("Silently signed in! Welcome " + PlayGamesPlatform.Instance.localUser.userName);
				} else {
					Debug.Log ("Oh... we're not signed in.");
				}
			}, true);
		} else {
			Debug.Log("We're already signed in");
		}
	}
	
	void OnMatchStarted(bool success, TurnBasedMatch match) {
		if (success) {
			Debug.Log ("Match Started");
			Application.LoadLevel("DinoFighter2");
//			PlayGamesPlatform.Instance.
		} else {
			// show error message
		}
	}
	
}
