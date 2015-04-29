using UnityEngine;
using System.Collections;
using GooglePlayGames.BasicApi.Multiplayer;

public class MultiSceneMessenger : MonoBehaviour {

	public GameType matchType;
	public TurnBasedMatch onlineMatch;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}
	
}
