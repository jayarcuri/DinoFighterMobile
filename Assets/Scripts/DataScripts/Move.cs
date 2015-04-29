using System.Collections;

public abstract class Move
{
	public MoveSet MoveType; //Could be Block, Attack, Throw, etc.
	public string name;
	public int Cost = 0;

	public abstract void YieldResults (Move move, out bool winner, out int outDamage, out int MeterGain);

	public abstract void YieldWinResults(out int winDamage, out int damageRange, out int winMeterGain);

}

