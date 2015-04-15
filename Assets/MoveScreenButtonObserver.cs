using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveScreenButtonObserver : MonoBehaviour {

	Button[] listeners;
	Image[] listenerApperances;
	Color defaultColor;
	public Color nonselectedColor;
	

	void Start () {
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Moves");
		listeners = new Button [temp.Length];
		listenerApperances = new Image[temp.Length];

		for (int i = 0; i < temp.Length; i++) {
			print(temp[i].name);
			listeners[i] = temp[i].GetComponent<Button>();
			listenerApperances [i] = temp[i].GetComponent<Image> ();
		}


	}

	public void buttonClickedEvent(Button callingButton){
		for (int i = 0; i < listeners.Length; i++) {
			if(listeners[i] != callingButton)
				listenerApperances[i].color = nonselectedColor;
			else
				listenerApperances[i].color = defaultColor;
	}
					}

	public void resetButtonColors(){
		foreach (Image img in listenerApperances) {
			img.color = defaultColor;
		}
	}


/*	void addButton(Button newButton){

	}

void removeButton(Button unsubButton){

}*/


}
