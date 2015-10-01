using System;
using System.Collections;

public class AIPlayer : Character
{
	Random randomGen;
	Character myCharacter;

	public AIPlayer(int randomSeed, Character character){
		randomGen = new Random (randomSeed);
		myCharacter = character;
	                                                     }

	public override string GetMoveName ()
	{
		int newMove;

		if (GetMeter () > 9)
			newMove = randomGen.Next (0, myCharacter.Moveset.Length);
		else if(GetMeter() > 4)
			newMove = randomGen.Next (0, myCharacter.Moveset.Length-1);
		else
			newMove = randomGen.Next (0, myCharacter.Moveset.Length-2);

		return myCharacter.Moveset [newMove].name;
	}
	
	public override CharacterType GetCharacterType()
	{
		return CharacterType.AI;
	}

	public override void AddMeter(int amount){
		myCharacter.AddMeter(amount);
	}
	
	public override int GetMeter(){
		return myCharacter.GetMeter();
	}
	
	public override void TakeDamage(int dmg){
		myCharacter.TakeDamage(dmg);
	}
	
	public override int GetHealth(){
		return myCharacter.GetHealth();
	}

}

