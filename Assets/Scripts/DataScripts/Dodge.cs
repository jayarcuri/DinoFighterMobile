using System.Collections;

public class Dodge : Move
{
	int Damage; //Base damage
	//int DamageRange;	//extra possible damage
	//Random r;
	
	public Dodge(int damage/*, int damageRange*/){
		name = MoveType = "Dodge";
		Damage = damage;
		//DamageRange = damageRange;
		//r = new Random();
	}
	public override void YieldResults(Move move, out bool winner, out int outDamage, out int MeterGain){	//Returns damage to be done to opponent and grants character meter
		switch (move.MoveType) {
			
		default:
			outDamage = 0;
			MeterGain = 0;
			winner = false;
			break;
			
		case "Attack":
			outDamage = Damage;
			MeterGain = 0;
			winner = true;
			break;
		}
	}
}