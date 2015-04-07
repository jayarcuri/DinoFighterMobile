using System.Collections;

public class Burst : Move
{
	
	public Burst(){
		MoveType = "Burst";
	}
	public override void YieldResults(Move move, out bool winner, out int outDamage, out int MeterGain){	//Returns damage to be done to opponent and grants character meter
		switch (move.MoveType) {
			
		default:
			outDamage = 0;
			MeterGain = 0;
			winner = false;
			break;
			
		case "Attack":
		case "Throw":
			outDamage = 0;
			MeterGain = 2;
			winner = true;
			break;
		}
	}
}