using System.Collections;
using System;

public class KyoryuSuper : Move
{
	int Damage, DamageRange;	//Base damage, extra possible damage
	Random r;
	
	public KyoryuSuper(int damage, int damageRange, int mySeed){
		name = "Super";
		MoveType = MoveSet.Attack;	//Still an attack
		Damage = damage;
		DamageRange = damageRange;
		Cost = 10;
		r = new Random(mySeed);
	}

	public override void YieldResults(Move move, out bool winner, out int outDamage, out int MeterGain){		//Returns damage to be done to opponent
		switch (move.MoveType) {
			
		default:
			outDamage = 0;
			MeterGain = -10;
			winner = false;
			break;
			
		case MoveSet.Attack:
		case MoveSet.Throw:
			outDamage = Damage + r.Next(DamageRange+1);
			MeterGain = -10;
			winner = true;
			break;

		case MoveSet.Defend:
			outDamage = (Damage + r.Next(DamageRange+1))/2;	//Deals 1/2 damage versus block
			MeterGain = -10;
			winner = false;
			break;
		}
	}
}