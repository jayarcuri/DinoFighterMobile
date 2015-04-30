using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using System;
//using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//using UnityEngine.UI;
using GooglePlayGames.BasicApi.Multiplayer;

[Serializable]
public class GameState {
	Int32 round; 
	Int32 turn;
	Move[] moves = new Move[2];
	
	public static GameState FromByteArray(Byte[] array) {
		MemoryStream stream = new MemoryStream(array);
		BinaryReader reader = new BinaryReader(stream);
		GameState state = new GameState();
		
		return state;
	}
	
	public static byte[] ToByteArray( GameState bundle) {
		MemoryStream stream = new MemoryStream(32);
		BinaryWriter writer = new BinaryWriter(stream);
		writer.Write(bundle.round);
		writer.Write(bundle.turn);
		writer.Write((Int32) bundle.moves[0].MoveType);
		writer.Write((Int32) bundle.moves[1].MoveType);
		return stream.GetBuffer();
	}
	
	public int getTurn() {
		return turn;
	}
	
	public int getRound() {
		return round;
	}
	
	
}
