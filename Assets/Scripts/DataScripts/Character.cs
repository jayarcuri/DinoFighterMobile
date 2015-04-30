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

	public abstract string GetMoveName ();
	
	public abstract CharacterType GetCharacterType ();

public virtual void AddMeter(int amount){
		Meter += amount;
		if (Meter > 10)
			Meter = 10;
	}

	public virtual int GetMeter(){
		return Meter;
	}

	public virtual void TakeDamage(int dmg){
		Health -= dmg;
	}

	public virtual int GetHealth(){
		return Health;
	}

	public virtual bool HasBurst(){
		return !HasUsedBurst;
	}
	
	public virtual void UseBurst(){
		HasUsedBurst = true;
	}
	
	public Move GetMove(MoveSet type){
		switch(type) {
		case MoveSet.Attack: GetAttack();
		case MoveSet.Burst: GetBurst();
		case MoveSet.Defend: GetDefend();
		case MoveSet.Dodge: GetDodge();
		case MoveSet.Super: GetSuper();
		case MoveSet.Throw: GetThrow();
		}
	}

	public Move GetAttack(){
		return Moveset [(int)MoveSet.Attack];
	}

	public Move GetDefend(){
		return Moveset [(int)MoveSet.Defend];
	}

	public Move GetDodge(){
		return Moveset [(int)MoveSet.Dodge];
	}

	public Move GetThrow(){
		return Moveset [(int)MoveSet.Throw];
	}

	public Move GetBurst(){
		return Moveset [(int)MoveSet.Burst];
	}

	public Move GetSuper(){
		return Moveset [(int)MoveSet.Super];
	}

}

