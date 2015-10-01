using UnityEngine;
using System.Collections;

public class RexPuppet : CharacterPuppet
{
	public Animation characterAnimations;
	public Transform characterHolder;
	public Transform HomeSpike;
	public Transform AttackClashSpike;
	public Transform MidpointSpike;

	public override IEnumerator Attack (string winLoseOrClash){
		switch (winLoseOrClash) {
		case "Win":
			characterAnimations.PlayQueued ("DashStart", QueueMode.PlayNow);
			characterAnimations.PlayQueued ("Dash", QueueMode.CompleteOthers);
			characterAnimations.PlayQueued ("Dash", QueueMode.CompleteOthers);
			characterAnimations.PlayQueued ("DashEnd", QueueMode.CompleteOthers);
			float goFor = 3;
			float elapsed = 0;
			print("check one");
			while(elapsed < goFor){
				print ("check 2");
				if(elapsed < goFor/2)
			{
					print ("check 3");
					characterHolder.position = Vector3.Lerp(HomeSpike.position, MidpointSpike.position, elapsed*2/goFor);
					print ((elapsed*2)/goFor);
			}
				if(elapsed > goFor/2){
					print ("check 4");
					characterHolder.position = Vector3.Lerp(MidpointSpike.position, HomeSpike.position, elapsed*2/goFor - 1f);
					print (elapsed*2/goFor - .5f);}
					elapsed += Time.deltaTime;
				//print (elapsed);
					yield return new WaitForEndOfFrame();}
			break;
		default:
			yield return new WaitForEndOfFrame();
			break;
		}
	}
	
	public override IEnumerator Burst (string winLoseOrClash){yield return new WaitForEndOfFrame();}
	
	public override IEnumerator Dodge (string winLoseOrClash){yield return new WaitForEndOfFrame();}
	
	public override IEnumerator Block (string winLoseOrClash){yield return new WaitForEndOfFrame();}
	
	public override IEnumerator GuardBreak (string winLoseOrClash){yield return new WaitForEndOfFrame();}
	
	public override IEnumerator Super (string winLoseOrClash){yield return new WaitForEndOfFrame();}

}

