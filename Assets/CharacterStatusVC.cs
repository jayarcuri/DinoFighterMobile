using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStatusVC : MonoBehaviour {

	public RectTransform meterBar;
	public RectTransform healthBar;
	public Text playerName;
	public Text healthPercentage;
	public bool isPlayer2;
	float playerMaxHealth;
	float currentPlayerHealth;
	float currentPlayerMeter = 0;
	// Use this for initialization
	void Start () {
		if(!isPlayer2)
		meterBar.anchorMax = new Vector3 (0, 1);
		else
		meterBar.anchorMin = new Vector3 (1, 0);
	}

	public void SetPlayerMaxHealth(int maxHealth){
		//print ("Max: " + maxHealth);
		playerMaxHealth = maxHealth;
		currentPlayerHealth = maxHealth;
	}

	public void SetPlayerName(string name){
		playerName.text = name;
	}
	
	public void UpdateStatus(int healthDelta, int meterDelta){
		if (healthDelta != 0) {
			currentPlayerHealth -= healthDelta;
			if(!isPlayer2)
				healthBar.anchorMax = new Vector2(Mathf.Lerp(0, 1, currentPlayerHealth/playerMaxHealth), 1);
			else
				healthBar.anchorMin = new Vector2(Mathf.Lerp(1, 0, currentPlayerHealth/playerMaxHealth), 0);
			healthPercentage.text = currentPlayerHealth.ToString() + "%";
		}
		if (meterDelta != 0) {
			print (meterDelta + " more meter!!");
			currentPlayerMeter += meterDelta;
			if(!isPlayer2)
				meterBar.anchorMax = new Vector2(Mathf.Lerp(0, 1, currentPlayerMeter/10), 1);
			else
				meterBar.anchorMin = new Vector2(Mathf.Lerp(1, 0, currentPlayerMeter/10), 0);
		}
	}
}
