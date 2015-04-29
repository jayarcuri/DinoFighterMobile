using System.Collections;

public class Burst : Move
{
	
	public Burst(){
		name = "Burst";
		MoveType = MoveSet.Burst;
	}
	public override void YieldResults(Move move, out bool winner, out int outDamage, out int MeterGain){	//Returns damage to be done to opponent and grants character meter
		switch (move.MoveType) {
			
		default:
			outDamage = 0;
			MeterGain = -5;
			winner = false;
			break;
			
		case MoveSet.Attack:
		case MoveSet.Throw:
			outDamage = 0;
			MeterGain = -4;
			winner = true;
			break;
		}
	}

	public override void YieldWinResults(out int winDamage, out int damageRange, out int winMeterGain){
		winDamage = 0;
		damageRange = 0;
		winMeterGain = 1;
	}
}