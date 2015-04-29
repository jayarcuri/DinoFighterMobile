using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveTooltip : MonoBehaviour {

	public Text moveName;
	public Text[] moveValues;
	public Text[] moveValueTypes;
	public Color[] moveColors;	//Red (damage color) is index 0, teal (meter color) is index 1)
	public FightDelegate fightDelegate;
	Image tooltipFrame;

	void Start(){
		tooltipFrame = gameObject.GetComponent<Image> ();
	}

	
	public void DisplayMove(){
		Move move = fightDelegate.YieldCurrentMove (fightDelegate.GetTurn());
		tooltipFrame.enabled = true;
		moveName.text = move.name;

		int winDamage, dmgSpread, winMeter;
		move.YieldWinResults (out winDamage, out dmgSpread, out winMeter);

		if (winDamage > 0 && winMeter > 0) {
			if (dmgSpread > 0) {
				moveValues [0].text = winDamage + "-" + dmgSpread;
				moveValues [0].color = moveColors [0];
			} else {
				moveValues [0].text = winDamage.ToString();
				moveValues [0].color = moveColors [0];
			}
			moveValueTypes [0].text = "Damage";
			moveValues [1].text = winMeter.ToString();
			moveValues [1].color = moveColors [1];
			moveValueTypes [1].text = "Meter";
		} else if (winDamage > 0) {
			if (dmgSpread > 0) {
				moveValues [0].text = winDamage + "-" + dmgSpread;
				moveValues [0].color = moveColors [0];
			} else {
				moveValues [0].text = winDamage.ToString();
				moveValues [0].color = moveColors [0];
			}
			moveValueTypes [0].text = "Damage";
			moveValues[1].text = "";
			moveValueTypes[1].text = "";

		} else if (winMeter > 0) {
			moveValues [0].text = winMeter.ToString();
			moveValues [0].color = moveColors [1];
			moveValueTypes [0].text = "Meter";
			moveValues[1].text = "";
			moveValueTypes[1].text = "";
		}
	}

	public void ClearTooltip(){
		moveName.text = "";
		moveValues[0].text = "";
		moveValues[1].text = "";
		moveValueTypes[0].text = "";
		moveValueTypes[1].text = "";
		tooltipFrame.enabled = false;
	}
}
