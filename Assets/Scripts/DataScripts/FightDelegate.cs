using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

[Serializable]
public class FightDelegate : MonoBehaviour{

	Character[] Fighters;
	string MoveForCurrentTurn;
	Move[] Moves;
	int Turn;
	public CharacterStatusVC[] characterGUI;
	public GUIViewController MyGUI;
	public bool testing;
	public Text InfoText;
	public Text ResultsText;
	public Text OperatorText;
	public Image[] MoveIconsForHistory;
	Image[] CurrentMoveIcons;
	
//	public FightDelegate() {
//	
//	}
	
	public static FightDelegate FromByteArray(Byte[] array) {
		return null;
	}
	
	public static byte[] ToByteArray( FightDelegate bundle) {
		return null;
	}

	void Start(){
		Fighters = new Character[2];
		CurrentMoveIcons = new Image[2];
		Moves = new Move[2];
		Turn = 0;
		if (testing) {
			Fighters[0] = new KyoryuCharacter(3);
			characterGUI [0].SetPlayerMaxHealth (Fighters [0].GetHealth());
			Fighters[1] = new KyoryuCharacter(7);
			characterGUI [1].SetPlayerMaxHealth (Fighters [1].GetHealth());
		}
		SetupGUI ();
	}

	public void SetCharacter(int playerNumber, Character character){
		Fighters [playerNumber] = character;
		characterGUI [playerNumber].SetPlayerMaxHealth (Fighters [playerNumber].GetHealth());
	}

	public void SetCurrentMove(string currentMove){	//Called every time a UI button for a move is pressed
		MoveForCurrentTurn = currentMove;
	}

	public void SetCurrentMoveIcon(Image currentImg){
		CurrentMoveIcons [Turn] = currentImg;
	}
	
	private Move YieldCurrentMove(){ //Called internally when a turn is incremented

		switch (MoveForCurrentTurn) {
		
		case "attack":
		case "Attack":
			return Fighters [Turn].GetAttack ();

		case "defend":
		case "Defend":
			return Fighters [Turn].GetDefend ();

		case "dodge":
		case "Dodge":
			return Fighters [Turn].GetDodge ();

		case "throw":
		case "Throw":
			return Fighters [Turn].GetThrow ();

		case "burst":
		case "Burst":
			return Fighters [Turn].GetBurst ();

		case "super":
		case "Super":
			return Fighters [Turn].GetSuper ();

		default:
			return null;
		}
	}

	public void IncrementTurn(){
		Moves [Turn] = YieldCurrentMove();
		Turn += 1;
		Turn = Turn % 2;
		//TurnText.text = "Player " + (Turn + 1) + "'s Turn";


		if (Turn == 0) {
			MyGUI.SummonHistoryFromMoves();
			bool P1IsWinner, P2IsWinner;
			P1IsWinner = P2IsWinner = false;
			int P1DamageToBeDealt, P2DamageToBeDealt, P1MeterGain, P2MeterGain;
			P1DamageToBeDealt = 0;
			P2DamageToBeDealt = 0;
			P1MeterGain = 0;
			P2MeterGain = 0;



			Moves[0].YieldResults(Moves[1], out P1IsWinner, out P1DamageToBeDealt, out P1MeterGain);
			Moves[1].YieldResults(Moves[0], out P2IsWinner, out P2DamageToBeDealt, out P2MeterGain);
			//Insert block for animation here
			//
			//
			if(P1IsWinner){
				InfoText.text = ("Player 1 won the round!");
				OperatorText.text = ">";
			}
			else if(P2IsWinner){
				InfoText.text = ("Player 2 won the round!");
				OperatorText.text = "<";
			}
			else if(P2DamageToBeDealt == 0 && P1DamageToBeDealt == 0){
				InfoText.text = ("No one won the round...");
				OperatorText.text = "=";
			}
			else if(P2DamageToBeDealt != 0 || P1DamageToBeDealt != 0){
				InfoText.text =  ("Player 1 & Player 2 clashed!");
				OperatorText.text = "=";
			}

			string resultsParagraph = "";

			if(P2DamageToBeDealt != 0){
				Fighters[0].TakeDamage(P2DamageToBeDealt);
				resultsParagraph += "Player 1 took " + P2DamageToBeDealt + " damage!\n";
			}
			if(P1DamageToBeDealt != 0){
				Fighters[1].TakeDamage(P1DamageToBeDealt);
				resultsParagraph += "Player 2 took " + P1DamageToBeDealt + " damage!\n";
			}
			if(P1MeterGain != 0){
				Fighters[0].AddMeter(P1MeterGain);
				resultsParagraph += "Player 1 gained " + P1MeterGain + " meter!\n";
			}
			if(P2MeterGain != 0){
				Fighters[1].AddMeter(P2MeterGain);
				resultsParagraph += "Player 2 gained " + P2MeterGain + " meter!\n";
			}

			characterGUI[0].UpdateStatus(P2DamageToBeDealt, P1MeterGain);
			characterGUI[1].UpdateStatus(P1DamageToBeDealt, P2MeterGain);
			MoveIconsForHistory[0].overrideSprite = CurrentMoveIcons[0].sprite;
			MoveIconsForHistory[1].overrideSprite = CurrentMoveIcons[1].sprite;

			ResultsText.text = resultsParagraph;


			for(int i = 0; i<Moves.Length-1;i++){
				if (Moves [i].MoveType == "Burst")
					Fighters [Turn].UseBurst ();
				Moves[i] = null;
			}
		}
		SetupGUI ();
	}

	public void SetupGUI(){
		MyGUI.SetView(Fighters[Turn]);
	}


}
