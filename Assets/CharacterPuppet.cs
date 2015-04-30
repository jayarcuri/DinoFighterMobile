using UnityEngine;
using System.Collections;

public abstract class CharacterPuppet : MonoBehaviour
{

	public abstract IEnumerator Attack (string winLoseOrClash);

	public abstract IEnumerator Burst (string winLoseOrClash);

	public abstract IEnumerator Dodge (string winLoseOrClash);

	public abstract IEnumerator Block (string winLoseOrClash);

	public abstract IEnumerator GuardBreak (string winLoseOrClash);

	public abstract IEnumerator Super (string winLoseOrClash);

}

