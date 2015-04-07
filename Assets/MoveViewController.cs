using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveViewController : MonoBehaviour {
	private Canvas[] MovePanels;

	void Awake(){
		MovePanels = new Canvas[2];
	}

	public void SetMovePanel(int index, string CharacterName){
		switch(CharacterName){
			/* case "Rex":
			 MovePanels[index] = Resources.Load(" ") as Canvas;
			break;*/
			/* case "Speedy":
			 MovePanels[index] = Resources.Load(" ") as Canvas;
			break;*/
			/* case "Frank":
			 MovePanels[index] = Resources.Load(" ") as Canvas;
			break;*/
		}
	}
		

	public void SetupView (int PlayerNumber, bool[] UsableMoves) {
	
	}

	public void ToggleGUIView (bool IsShowing){	//Make sure no funky behavior is happening here
		gameObject.GetComponent<Canvas> ().enabled = IsShowing;
	}
}
