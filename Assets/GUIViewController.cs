using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIViewController : MonoBehaviour
{
	public Button super;
	public Button burst;

	public void SetView(Character character){
		print ("Fuck you!");
		print ("Burst: " + character.HasBurst ().ToString() );
		print ("Meter is 10: " + (character.GetMeter () > 9).ToString() );
		burst.interactable = character.HasBurst ();
		super.interactable = (character.GetMeter () > 9);
	}
}

