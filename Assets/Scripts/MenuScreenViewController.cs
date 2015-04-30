using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScreenViewController : MonoBehaviour {
		
	public Text WinnerText;
	public Text RoundStats;
	public RectTransform[] characterBars;
	MultiSceneMessenger matchInfo;
	public bool showing = false;
	public FightDelegate fight;

	void Start(){
		matchInfo = new MultiSceneMessenger();
	}

	public void LoadMenu(){
		gameObject.GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;
//		WinnerText.text = winner + " won!!!";

//		int minutes = Mathf.FloorToInt(Time.time / 60f);
//		int seconds = Mathf.FloorToInt(Time.time - minutes * 60);

//		RoundStats.text = "Match Length: " + string.Format ("{0:0}:{1:00}", minutes, seconds) + "\n"
//							+ "Rounds: " + turns;
//			characterBars [0].anchoredPosition = new Vector2 (Screen.width * .08f, -Screen.height * .525f);
//			characterBars [1].anchoredPosition = new Vector2 (-Screen.width * .08f, -Screen.height * .525f);
		
	}
	
	public void CloseMenu() {
		gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2(0f, 956);
	}
	
	public void MainMenu() {
		Application.LoadLevel("MainMenu");
	}
	
	public void Rematch() {
		matchInfo.matchType = fight.type;
		Application.LoadLevel("DinoFighter2");
	}

}
