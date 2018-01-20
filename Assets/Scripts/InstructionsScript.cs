using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionsScript : MonoBehaviour {

	int pageNumber;

	public string username;
	public int lifes;

	public GameObject GC;
	GameController gc;

	public GameObject Player;
	CharacterScript player;

	public Text jumpingText;
	public Text pointsText;
	public Text collectText;
	public GameObject pointslogo;
	public GameObject ipadlogo;

	// Use this for initialization
	void Start () {
		pageNumber = 1;
		gc = GC.GetComponent<GameController> ();
		player = Player.GetComponent<CharacterScript> ();

		jumpingText.gameObject.SetActive (false);
		pointsText.gameObject.SetActive (false);
		pointslogo.gameObject.SetActive (false);
		collectText.gameObject.SetActive (false);
		ipadlogo.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (pageNumber == 1) {
			
			jumpingText.gameObject.SetActive (true);
			pointsText.gameObject.SetActive (false);
			pointslogo.gameObject.SetActive (false);
			collectText.gameObject.SetActive (false);
			ipadlogo.gameObject.SetActive (false);

		} else if (pageNumber == 2) {

			jumpingText.gameObject.SetActive (false);
			pointsText.gameObject.SetActive (true);
			pointslogo.gameObject.SetActive (true);
			collectText.gameObject.SetActive (false);
			ipadlogo.gameObject.SetActive (false);

		} else if (pageNumber == 3) {

			jumpingText.gameObject.SetActive (false);
			pointsText.gameObject.SetActive (false);
			pointslogo.gameObject.SetActive (false);
			collectText.gameObject.SetActive (true);
			ipadlogo.gameObject.SetActive (true);
		} 

		
	}

	public void loadFullGame()
	{
		gc.userName = username;
		player.lifes = lifes;
		SceneManager.LoadScene ("Runner");
	}

	public void changePageForward()
	{
		pageNumber++;
		if (pageNumber > 3) {
			pageNumber = 1;
		}
	}

	public void changePageBackward()
	{
		pageNumber--;
		if (pageNumber < 1) {
			pageNumber = 3;
		}
	}


}
