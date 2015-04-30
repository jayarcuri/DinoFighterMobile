using UnityEngine;
using System.Collections;

public class TesterScript : MonoBehaviour {

	public CharacterPuppet testPuppet;

	void Update(){
		if (Input.GetKeyDown (KeyCode.A))
			StartCoroutine(testPuppet.Attack ("Win"));
				}
}
