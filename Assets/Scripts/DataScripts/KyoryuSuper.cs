using System.Collections;
using System;

public class KyoryuSuper : Move
{
	int Damage, DamageRange;	//Base damage, extra possible damage
	Random r;
	
	public KyoryuSuper(int damage, int damageRange){
		MoveType = "Attack";	//Still an attack
		Damage = damage;
		DamageRange = damageRange;
		Cost = 10;
		r = new Random();
	}

	public override void YieldResults(Move move, out bool winner, out int outDamage, out int MeterGain){		//Returns damage to be done to opponent
		switch (move.MoveType) {
			
		default:
			outDamage = 0;
			MeterGain = -10;
			winner = false;
			break;
			
		case "Attack":
		case "Throw":
			outDamage = Damage + r.Next(0, DamageRange+1);
			MeterGain = -10;
			winner = true;
			break;

		case "Defend":
			outDamage = (Damage + r.Next(0, DamageRange+1))/3;	//Deals 1/3 damage versus block
			MeterGain = -10;
			winner = false;
			break;
		}
	}
}