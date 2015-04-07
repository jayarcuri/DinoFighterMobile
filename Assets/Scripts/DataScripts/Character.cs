using System.Collections;

public abstract class Character
{
	/*
	0 - Attack
	1 - Defend
	2 - Dodge
	3 - Throw
	4 - Burst
	Special	<- Being added later
	5 - Super*/

	public Move[] Moveset;
	public int Meter, Health;
	bool HasUsedBurst = false;

public void AddMeter(int amount){
		Meter += amount;
		if (Meter > 10)
			Meter = 10;
	}

	public int GetMeter(){
		return Meter;
	}

	public void TakeDamage(int dmg){
		Health -= dmg;
	}

	public int GetHealth(){
		return Health;
	}

	public Move GetAttack(){
		return Moveset [0];
	}

	public Move GetDefend(){
		return Moveset [1];
	}

	public Move GetDodge(){
		return Moveset [2];
	}

	public Move GetThrow(){
		return Moveset [3];
	}

	public Move GetBurst(){
		return Moveset [4];
	}

	public Move GetSuper(){
		return Moveset [5];
	}

	public bool HasBurst(){
		return !HasUsedBurst;
	}

	public void UseBurst(){
		HasUsedBurst = true;
	}
	

}

