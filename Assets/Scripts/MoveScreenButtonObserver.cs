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
			listeners[i] = temp[i].GetComponent<Button>();
			listenerApperances [i] = temp[i].GetComponent<Image> ();
		}
		defaultColor = listeners [0].colors.normalColor;
	}

	public void buttonClickedEvent(Button callingButton){
		for (int i = 0; i < listeners.Length; i++) {
			if(listeners[i] != callingButton && listeners[i].IsInteractable()){
				ColorBlock cb = listeners[i].colors;
				cb.normalColor = nonselectedColor;
				listeners[i].colors = cb;
			}
			else if(listeners[i].IsInteractable()){
				ColorBlock cb = listeners[i].colors;
				cb.normalColor = defaultColor;
				listeners[i].colors = cb;
			}
		}
	}
	
	void onGUI() {
		Event e = Event.current;
		if(e.keyCode == KeyCode.Escape) {
		
		}
	}

	public void resetButtonColors(){
		foreach (Button listener in listeners) {
			ColorBlock cb = listener.colors;
			cb.normalColor = defaultColor;
			listener.colors = cb;
		}
	}


}
