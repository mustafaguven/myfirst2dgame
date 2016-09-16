using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {

	public Sprite flagOpen, flagClosed;
	private SpriteRenderer spriteRenderer;
	private bool isSoundPlayed = false;
	private AudioSource checkPointSound;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		checkPointSound = GameObject.Find ("CheckPointSound").GetComponent<AudioSource> ();
	}

	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if (!isSoundPlayed) {
				checkPointSound.Play ();
			}
			spriteRenderer.sprite = flagOpen;
			isSoundPlayed = true;
		}
	}
}
