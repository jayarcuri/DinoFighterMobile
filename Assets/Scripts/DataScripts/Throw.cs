using System.Collections;
using System;

public class Throw : Move
{
	int Damage, DamageRange;	//Base damage, extra possible damage
	Random r;
	
	public Throw(int damage, int damageRange, int mySeed){
		name = "Throw";
		MoveType = MoveSet.Throw;
		Damage = damage;
		DamageRange = damageRange;
		r = new Random(mySeed);
	}
	public override void YieldResults(Move move, out bool winner, out int outDamage, out int MeterGain){	//Returns damage to be done to opponent and grants character meter
		switch (move.MoveType) {
			
		default:
			outDamage = 0;
			MeterGain = 0;
			winner = false;
			break;
			
		case MoveSet.Defend:
		case MoveSet.Dodge:
			outDamage = Damage + r.Next(DamageRange+1);
			MeterGain = 1;
			winner = true;
			break;
		}
	}
}

