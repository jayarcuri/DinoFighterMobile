using System.Collections;

public class KyoryuCharacter : Character
{
	/*
	0 - Attack
	1 - Defend
	2 - Dodge
	3 - Throw
	4 - Burst
	Special		<- Being added later
	5 - Super*/

	public KyoryuCharacter(int seed){
		//seed needed to induce actual randomness for variable attacks
		Meter = 0;
		Health = 100;
		Moveset = new Move[]{new Attack(20, 5, seed), new Defend(), new Dodge(19), new Throw(15, 10, seed), 
			new Burst(), new KyoryuSuper(25, 10, seed)};
	}

	public override string GetMoveName ()
	{
		throw new System.NotImplementedException ();
	}
	
	public override CharacterType GetCharacterType ()
	{
		return CharacterType.Kyoryu;
	}
	
	

}

