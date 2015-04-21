using System.Collections;
using System;

public class Attack : Move
{
	int Damage, DamageRange;	//Base damage, extra possible damage
	Random r;

	public Attack(int damage, int damageRange, int mySeed){
		name = MoveType = "Attack";
		Damage = damage;
		DamageRange = damageRange;
		r = new Random(mySeed);
	}
	public override void YieldResults(Move move, out bool winner, out int outDamage, out int MeterGain){	//Returns damage to be done to opponent and grants character meter
		Console.WriteLine("Yielding triggered.");
		switch (move.MoveType) {
			
		case "Attack":
		case "Throw":
			Console.WriteLine("attack has hit.");
			outDamage = Damage + r.Next(DamageRange+1);
			MeterGain = 1;
			winner = (move.MoveType == "Throw");
			break;

		default:
			outDamage = 0;
			MeterGain = 0;
			winner = false;
			break;
		}
	}
}

