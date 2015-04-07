﻿using UnityEngine;
using System.Collections;

public class ActionSelectionController : MonoBehaviour {

	public GameObject childPanel;
	public ActionSelectionController otherButton;

public void Deactivate(){
		childPanel.SetActive (false);
	}

public void Activate(){
		otherButton.Deactivate ();
		childPanel.SetActive (true);
	}

	public void SetChildPanel(GameObject newChild){
		Deactivate ();
		childPanel = newChild;
	}
}
