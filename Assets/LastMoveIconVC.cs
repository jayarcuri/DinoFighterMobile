using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LastMoveIconVC : MonoBehaviour {
	public Sprite[] moveSprites;
	public Image[] moveIcons;

	/*
	0 - Attack
	1 - Defend
	2 - Dodge
	3 - Throw
	4 - Burst
	Special		<- Being added later
	5 - Super*/


	public void setIcon(int turn, int iconIndex){
		moveIcons[turn].sprite = moveSprites [iconIndex];
	}
}
