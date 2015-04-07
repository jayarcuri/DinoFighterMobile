using System.Collections;

public abstract class Move
{
	public string MoveType; //Could be Block, Attack, Throw, etc.
	public int Cost = 0;

	public virtual void YieldResults(Move move, out bool winner, out int outDamage, out int MeterGain){
		winner = false;
		outDamage = 0;
		MeterGain = 0;
	}

}

