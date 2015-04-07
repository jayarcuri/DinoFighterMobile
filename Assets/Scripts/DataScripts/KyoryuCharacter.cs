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

	public KyoryuCharacter(){
		Meter = 0;
		Health = 100;
		Moveset = new Move[]{new Attack(15, 10), new Defend(), new Dodge(15), new Throw(15, 10), 
			new Burst(), new KyoryuSuper(20, 10)};
	}
}

