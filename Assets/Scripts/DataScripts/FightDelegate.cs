using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FightDelegate : MonoBehaviour {

	Character[] Fighters;
	string MoveForCurrentTurn;
	Move[] Moves;
	int Turn;
	public GUIViewController MyGUI;
	public bool testing;
	public Text TurnText;
	public Text[] HealthTotals, MeterTotals;
	public Text InfoText;

	void Start(){
		Fighters = new Character[2];
		Moves = new Move[2];
		Turn = 0;
		if (testing) {
			Fighters[0] = new KyoryuCharacter();
			Fighters[1] = new KyoryuCharacter();
		}
		SetupGUI ();
	}

	public void SetCharacter(int playerNumber, Character character){
		Fighters [playerNumber] = character;
	}

	public void SetCurrentMove(string currentMove){	//Called every time a UI button for a move is pressed
		print (currentMove + " being set.");
		MoveForCurrentTurn = currentMove;
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
		TurnText.text = "Player " + (Turn + 1) + "'s Turn";


		if (Turn == 0) {
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
			if(P1IsWinner)
				InfoText.text = ("Player 1's " + Moves[0].MoveType + " beat Player 2's " + Moves[1].MoveType+ "!");
			if(P2IsWinner)
				InfoText.text = ("Player 2's " + Moves[0].MoveType + " beat Player 1's " + Moves[1].MoveType+ "!");
			if(P2DamageToBeDealt == 0 && P1DamageToBeDealt == 0)
				InfoText.text = ("Player 1's " + Moves[0].MoveType + " & Player 2's " + Moves[1].MoveType+ " negated!");
			else if(P2DamageToBeDealt != 0 || P1DamageToBeDealt != 0)
				InfoText.text =  ("Player 1's " + Moves[0].MoveType + " & Player 2's " + Moves[1].MoveType+ " clashed!");


			if(P2DamageToBeDealt != 0){
				Fighters[0].TakeDamage(P2DamageToBeDealt);
			HealthTotals[0].text = Fighters[0].GetHealth().ToString();
			}
			if(P1DamageToBeDealt != 0){
				Fighters[1].TakeDamage(P1DamageToBeDealt);
				HealthTotals[1].text = Fighters[1].GetHealth().ToString();
			}
			if(P1MeterGain != 0){
				Fighters[0].AddMeter(P1MeterGain);
				MeterTotals[0].text = Fighters[0].GetMeter().ToString();
			}
			if(P2MeterGain != 0){
				Fighters[1].AddMeter(P2MeterGain);
				MeterTotals[1].text = Fighters[1].GetMeter().ToString();
			}

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
