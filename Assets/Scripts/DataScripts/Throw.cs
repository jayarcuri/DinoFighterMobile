using System.Collections;
using System;

public class Throw : Move
{
	int Damage, DamageRange;	//Base damage, extra possible damage
	Random r;
	
	public Throw(int damage, int damageRange){
		MoveType = "Throw";
		Damage = damage;
		DamageRange = damageRange;
		r = new Random();
	}
	public override void YieldResults(Move move, out bool winner, out int outDamage, out int MeterGain){	//Returns damage to be done to opponent and grants character meter
		switch (move.MoveType) {
			
		default:
			outDamage = 0;
			MeterGain = 0;
			winner = false;
			break;
			
		case "Defend":
		case "Dodge":
			outDamage = Damage + r.Next(0, DamageRange+1);
			MeterGain = 1;
			winner = true;
			break;
		}
	}
}

