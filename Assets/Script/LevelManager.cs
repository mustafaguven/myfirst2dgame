using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
 
	private int waitToRespawn = 1;
	private PlayerController thePlayer;
	public GameObject deathExplotion;
	private int coins;
	private Text coinText;


	void Start () {
		thePlayer = FindObjectOfType<PlayerController>();
		coinText = GameObject.Find ("CoinText").GetComponent<Text> ();
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
		yield return new WaitForSeconds (waitToRespawn);
		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.gameObject.SetActive (true); 
	}

	public void addCoins(int coinValue){
		coins += coinValue;
		showCoinCount();
	}

	private void showCoinCount() {
		coinText.text = "Coins: " + coins;
	}
}
