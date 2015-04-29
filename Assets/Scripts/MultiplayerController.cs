using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;

public class MultiplayerController {

	private static MultiplayerController _instance = null;
	
	const int MinOpponents = 2;
	const int MaxOpponents = 2;
	const int Variant = 0;
	
	private TurnBasedMatch mIncomingMatch;
	
	MultiSceneMessenger matchInfo;
	
	private MultiplayerController() {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			// enables saving game progress.
			.EnableSavedGames()
				// registers a callback to handle game invitations received while the game is not running.
				.WithInvitationDelegate(OnInvitationReceived)
				// registers a callback for turn based match notifications received while the
				// game is not running.
				.WithMatchDelegate(OnGotMatch)
				.Build();
		PlayGamesPlatform.InitializeInstance(config);
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
	
	public void SignInAndStartQuickGame(MultiSceneMessenger info) {
		matchInfo = info;
		if (! PlayGamesPlatform.Instance.localUser.authenticated) {
			Social.localUser.Authenticate((bool success) => {
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
			Debug.Log(match.Participants);
			matchInfo.matchType = GameType.online;
			matchInfo.onlineMatch = match;
			Application.LoadLevel("DinoFighter2");
//			PlayGamesPlatform.Instance.
		} else {
			// show error message
		}
	}
	
	public void OnInvitationReceived(Invitation invitation, bool shouldAutoAccept) {
	}
	
	void OnGotMatch(TurnBasedMatch match, bool shouldAutoLaunch) {
		if (shouldAutoLaunch) {
			// if shouldAutoLaunch is true, we know the user has indicated (via an external UI)
			// that they wish to play this match right now, so we take the user to the
			// game screen without further delay:
			OnMatchStarted(true, match);
		} else {
			// if shouldAutoLaunch is false, this means it's not clear that the user
			// wants to jump into the game right away (for example, we might have received
			// this match from a background push notification). So, instead, we will
			// calmly hold on to the match and show a prompt so they can decide
			mIncomingMatch = match;
		}
	}
	
	public void ShowSelectUI() {
		uint maxNumToDisplay = 5;
		bool allowCreateNew = false;
		bool allowDelete = true;
		
		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.ShowSelectSavedGameUI("Select saved game",
		                                      maxNumToDisplay,
		                                      allowCreateNew,
		                                      allowDelete,
		                                      OnSavedGameSelected);
	}
	
	void OnSavedGameSelected (SelectUIStatus status, ISavedGameMetadata game) {
		if (status == SelectUIStatus.SavedGameSelected) {
			Debug.Log("Game loaded");
		} else {
			Debug.Log("????"+status);
		}
	}
}
