using UnityEngine;
using System.Collections;

public class MultiSceneMessenger : MonoBehaviour {

	public GameType matchType;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}
	
}
