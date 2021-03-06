using System.Collections;

public class Defend : Move
{
	public Defend(){
		name = "Defend";
		MoveType = MoveSet.Defend;
	}
	public override void YieldResults(Move move, out bool winner, out int outDamage, out int MeterGain){	//Returns damage to be done to opponent and grants character meter
		switch (move.MoveType) {
			
		default:
			outDamage = 0;
			MeterGain = 0;
			winner = false;
			break;
			
		case MoveSet.Attack:
			outDamage = 0;
			MeterGain = 3;
			winner = true;
			break;
		}
	}

	public override void YieldWinResults(out int winDamage, out int damageRange, out int winMeterGain){
		winDamage = 0;
		damageRange = 0;
		winMeterGain = 3;
	}
}

