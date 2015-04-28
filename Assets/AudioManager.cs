using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public AudioSource gameMusicIntro;
	public AudioSource gameMusicLoop;
	
	void Start () {
		gameMusicIntro.Play ();
		gameMusicLoop.PlayScheduled (AudioSettings.dspTime + 4.8f);
	}

}
