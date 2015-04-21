using System.Collections;

public abstract class Move
{
	public string MoveType; //Could be Block, Attack, Throw, etc.
	public string name;
	public int Cost = 0;

	public abstract void YieldResults (Move move, out bool winner, out int outDamage, out int MeterGain);

}

