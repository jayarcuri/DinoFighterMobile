using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScreenViewController : MonoBehaviour {
		

	public bool showing = false;
	public FightDelegate fight;
	
	public void LoadMenu(){
		gameObject.GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;
		showing = true;
		
	}
	
	public void CloseMenu() {
		gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2(0f, 956);
		showing = false;
	}
	
	public void MainMenu() {
		Application.LoadLevel("MainMenu");
	}
	
	public void Rematch() {
		Application.LoadLevel("DinoFighter2");
	}

}
