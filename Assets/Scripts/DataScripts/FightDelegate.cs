using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using GooglePlayGames.BasicApi.Multiplayer;

[Serializable]
public class FightDelegate : MonoBehaviour{

	Character[] Fighters;
	string MoveForCurrentTurn;
	Move[] Moves;
	int Turn;
	int Rounds;
	string[] playerNames;
	public WinScreenViewController gameOverScreen;
	public GameObject BetweenTurnPopup;
	public CharacterStatusVC[] characterGUI;
	public LastMoveIconVC moveIcons;
	public GUIViewController MyGUI;
	public bool testing;
	public Text InfoText;
	public Text[] ResultsText;
	public Text OperatorText;
	public GameType type;
	bool started = false;
	
	public static FightDelegate FromByteArray(Byte[] array) {
		MemoryStream stream = new MemoryStream(array);
		BinaryReader reader = new BinaryReader(stream);
		FightDelegate fight = new FightDelegate();
		fight.type = (GameType)reader.ReadInt32();
		fight.Rounds = reader.ReadInt32();
		
		CharacterType type1 = (CharacterType)reader.ReadInt32();
		Character player1 = null;
		switch(type1) {
//			case CharacterType.AI:
//				player1 = new AIPlayer();
//				break;
			case CharacterType.Kyoryu:
				player1 = new KyoryuCharacter(3);
				break;
		}
		player1.Health = reader.ReadInt32();
		player1.Meter = reader.ReadInt32();
		fight.Moves = new Move[2];
//		fight.Moves[0] = player1.GetMoveName;
		
		CharacterType type2 = (CharacterType)reader.ReadInt32();
		Character player2 = null;
		switch(type2) {
		case CharacterType.AI:
			player2 = new AIPlayer(5, new KyoryuCharacter(7));
			break;
		case CharacterType.Kyoryu:
			player2 = new KyoryuCharacter(7);
			break;
		}
		player2.Health = reader.ReadInt32();
		player2.Meter = reader.ReadInt32();
		
		
		fight.Fighters = new Character[2];
		fight.Fighters[0] = player1;
		fight.Fighters[1] = player2;
		
		fight.started = true;
		
		return fight;
	}

	public static byte[] ToByteArray( FightDelegate bundle) {
		MemoryStream stream = new MemoryStream(48);
		BinaryWriter writer = new BinaryWriter(stream);
		writer.Write((int)bundle.type);
		writer.Write(bundle.Rounds);
		writer.Write(bundle.Turn);
		Character player1 = bundle.Fighters[0];
		writer.Write((int)player1.GetCharacterType());
		writer.Write(player1.GetHealth());
		writer.Write(player1.GetMeter());
		writer.Write((int) bundle.Moves[0].MoveType);
		Character player2 = bundle.Fighters[1];
		writer.Write((int)player2.GetCharacterType());
		writer.Write(player2.GetHealth());
		writer.Write(player2.GetMeter());
		writer.Write((int) bundle.Moves[1].MoveType);
		return stream.GetBuffer();
	}
	

	void Start(){

		if(!started) {
			if (GameObject.Find ("MatchInfo") != null) {	//If the match was set by a former screen, set this match to that type
				MultiSceneMessenger msm = GameObject.Find ("MatchInfo").GetComponent<MultiSceneMessenger> ();
					type = msm.matchType;
			}
	
			Moves = new Move[2];
			Turn = 0;
			Fighters = new Character[2];
	
			if (type == GameType.local) {
				playerNames = new string[]{"Player 1", "Player 2"};
				Fighters[0] = new KyoryuCharacter(3);
				Fighters[1] = new KyoryuCharacter(7);
			}
			if (type == GameType.ai) {
				playerNames = new string[]{"Player 1", "Com"};
				Fighters[0] = new KyoryuCharacter(3);
				Fighters[1] = new AIPlayer(5, new KyoryuCharacter(7));
			}
		}
		
		BetweenTurnPopup.SetActive (false);
		
		characterGUI [0].SetPlayerMaxHealth (Fighters [0].GetHealth());
		characterGUI [1].SetPlayerMaxHealth (Fighters [1].GetHealth());
		
		characterGUI [0].SetPlayerName(playerNames [0]);
		characterGUI [1].SetPlayerName(playerNames [1]);

		SetupGUI ();
	}
	
	void StartNetworkGame(TurnBasedMatch match) {
	
	}

	public void SetCharacter(int playerNumber, Character character){
		Fighters [playerNumber] = character;
		characterGUI [playerNumber].SetPlayerMaxHealth (Fighters [playerNumber].GetHealth());
	}

	public void SetCurrentMove(string currentMove){	//Called every time a UI button for a move is pressed
		MoveForCurrentTurn = currentMove;
	}

	public void SetCurrentMoveIcon(int index){

	}

	
	private Move YieldCurrentMove(int forCharacter){ //Called internally when a turn is incremented

		switch (MoveForCurrentTurn) {
		
		case "attack":
		case "Attack":
			moveIcons.setIcon(forCharacter, 0);
			return Fighters [Turn].GetAttack ();

		case "defend":
		case "Defend":
			moveIcons.setIcon(forCharacter, 1);
			return Fighters [Turn].GetDefend ();

		case "dodge":
		case "Dodge":
			moveIcons.setIcon(forCharacter, 2);
			return Fighters [Turn].GetDodge ();

		case "throw":
		case "Throw":
			moveIcons.setIcon(forCharacter, 3);
			return Fighters [Turn].GetThrow ();

		case "burst":
		case "Burst":
			moveIcons.setIcon(forCharacter, 4);
			return Fighters [Turn].GetBurst ();

		case "super":
		case "Super":
			moveIcons.setIcon(forCharacter, 5);
			return Fighters [Turn].GetSuper ();

		default:
			return null;
		}
	}

	public void IncrementTurn(){
		if (type == GameType.local) {
			Moves [Turn] = YieldCurrentMove (Turn);
			++Turn;
			Turn = Turn % 2;
		} 

		if(type == GameType.ai) {
			Moves[0] = YieldCurrentMove(0);
			MoveForCurrentTurn = Fighters[1].GetMoveName();
			Moves[1] = YieldCurrentMove(1);
		}


		if (Turn == 1) {
			BetweenTurnPopup.SetActive(true);
		}

		if (Turn == 0) {
			++Rounds;
			MyGUI.SummonHistoryFromMoves();	//brings in post-round screen
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
				InfoText.text = (playerNames[0] + " won the round!");
				OperatorText.text = ">";
			}
			else if(P2IsWinner){
				InfoText.text = (playerNames[1] + " won the round!");
				OperatorText.text = "<";
			}
			else if(P2DamageToBeDealt == 0 && P1DamageToBeDealt == 0){
				InfoText.text = ("No one won the round...");
				OperatorText.text = "=";
			}
			else if(P2DamageToBeDealt > 0 || P1DamageToBeDealt > 0){
				InfoText.text =  (playerNames[0] + " & " + playerNames[1] + " clashed!");
				OperatorText.text = "=";
			}

			string resultsParagraph1 = "";
			string resultsParagraph2 = "";

			if(P2DamageToBeDealt > 0 ){
				Fighters[0].TakeDamage(P2DamageToBeDealt);
				resultsParagraph1 += playerNames[0] + " took " + P2DamageToBeDealt + " damage!\n";
			}
			if(P1DamageToBeDealt > 0){
				Fighters[1].TakeDamage(P1DamageToBeDealt);
				resultsParagraph2 += playerNames[1] + " took " + P1DamageToBeDealt + " damage!\n";
			}
			if(P1MeterGain > 0){
				Fighters[0].AddMeter(P1MeterGain);
				resultsParagraph1 += playerNames[0] +  " gained " + P1MeterGain + " meter!\n";
			}
			if(P2MeterGain > 0){
				Fighters[1].AddMeter(P2MeterGain);
				resultsParagraph2 += playerNames[1] +  " gained " + P2MeterGain + " meter!\n";
			}

			characterGUI[0].UpdateStatus(P2DamageToBeDealt, P1MeterGain);
			characterGUI[1].UpdateStatus(P1DamageToBeDealt, P2MeterGain);


			ResultsText[0].text = resultsParagraph1;
			ResultsText[1].text = resultsParagraph2;
		}
		SetupGUI ();
	}

	public void SetupGUI(){
		MyGUI.SetView (Fighters [Turn], Turn+1);
	}

	public void SomeoneWon(){
		if (Fighters [0].Health == 0 && Fighters [1].Health == 0) {
			gameOverScreen.GameOver("No one", Rounds);
		} else if (Fighters [0].Health == 0) {
			gameOverScreen.GameOver("Player 2", Rounds);
		} else if (Fighters [1].Health == 0) {
			gameOverScreen.GameOver("Player 1", Rounds);
		}

	}


}
