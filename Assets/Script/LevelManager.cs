using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
 
	private int waitToRespawn = 1; 
	private int remainHeart = 3;
	private PlayerController thePlayer;
	public GameObject deathExplotion, gameOverScreen;
	private int coins;
	private Text coinText, gameOverText;
	private Image heart1, heart2, heart3;
	public Sprite emptyHeart; 
	private AudioSource coinSoundx1, coinSoundx5;


	void Start () {
		thePlayer = FindObjectOfType<PlayerController>();
		coinText = GameObject.Find ("CoinText").GetComponent<Text> ();
		gameOverScreen = GameObject.Find ("GameOverScreen");
		gameOverScreen.SetActive (false);
		heart1 = GameObject.Find ("Heart1").GetComponent<Image> ();
		heart2 = GameObject.Find ("Heart2").GetComponent<Image> ();
		heart3 = GameObject.Find ("Heart3").GetComponent<Image> (); 
		coinSoundx1 = GameObject.Find ("CoinSound1").GetComponent<AudioSource> ();
		coinSoundx5 = GameObject.Find ("CoinSound5").GetComponent<AudioSource> ();
		showCoinCount();
	}
 
	void Update () {
		
	}

	public void reSpawn(){
		StartCoroutine ("reSpawnCoroutine");
	}

	private IEnumerator reSpawnCoroutine() {
		thePlayer.gameObject.SetActive (false);
		Instantiate (deathExplotion, thePlayer.transform.position, thePlayer.transform.rotation);
		dead();
		if (remainHeart > 0) {
			yield return new WaitForSeconds (waitToRespawn);
			thePlayer.transform.position = thePlayer.respawnPosition;
			thePlayer.gameObject.SetActive (true); 
		} else {
			gameOverScreen.SetActive (true);
		}
	}

	public void addCoins(int coinValue){
		coins += coinValue;
		if (coinValue == 1) {
			coinSoundx1.Play ();	
		} else if (coinValue == 5) {
			coinSoundx5.Play ();
		}
		showCoinCount();
	}

	private void showCoinCount() {
		coinText.text = "Coins: " + coins;
	}

	private void dead(){
		remainHeart--;
		if (remainHeart == 2) {
			heart3.sprite = emptyHeart;
		} else if (remainHeart == 1) {
			heart2.sprite = emptyHeart;
		} else {
			heart1.sprite = emptyHeart;
		}
	}
}
