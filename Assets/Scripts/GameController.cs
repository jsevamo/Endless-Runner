using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	CharacterScript character;
	//public GameObject levelLoader;
	public Text pointsText;
	public Text hightScoreText;
	public Text lifeCountText;
	public Text oneHundred;
	public Text usernameText;
	public Button tryAgainButton;
	public Button QuitButton;
	public Button FinalQuitButton;
	public Text timePlayedText;

	public GameObject tryAgainScreen;
	public GameObject heartImage;
	public GameObject pointsImage;
	public GameObject timeImage;
	public Text pointsWhenLost;

	[HideInInspector] public float TotalPoints;
	[HideInInspector] public float HighScore;
	int numberOfTries;
	public float pointChangeSpeed = 0.3f;
	public bool deleteData = false;
	bool wantToTryAgain;
	[HideInInspector] public bool grabbedCollect = false;
	int amountOfPointsGained = 100;

	[HideInInspector] public float totalGameTime;

	public string userName = "user";

	int timePlayed;
	int timeForRun;

	public GameObject School1BG;
	public GameObject School2BG;
	public GameObject School3BG;

	public GameObject AfterLog;
	AfterLogin afterlog;




	// Use this for initialization
	void Start () {
		
		Time.timeScale = 1f;

		afterlog = AfterLog.GetComponent<AfterLogin> ();

		timeForRun = 999999999;
		timePlayed = 0;
		StartCoroutine(countOneSecond());
		hightScoreText.gameObject.SetActive (false);
		usernameText.gameObject.SetActive (false);
		tryAgainScreen.SetActive (false);
		pointsWhenLost.gameObject.SetActive (false);
		QuitButton.gameObject.SetActive (false);
		FinalQuitButton.gameObject.SetActive (false);

		usernameText.text = userName;

		character = GameObject.Find ("Player").GetComponent<CharacterScript> ();
		TotalPoints = 0;

		if (PlayerPrefs.HasKey("HighScore")) {

			HighScore = PlayerPrefs.GetFloat ("HighScore");
			hightScoreText.text = ("High Score: ") + Mathf.Round (HighScore).ToString ();

		} else if(deleteData == true) {
			HighScore = 0;
		}

		oneHundred.gameObject.SetActive(false);

		pointsText.text = 0.ToString();
		numberOfTries = 0;

		wantToTryAgain = false;





		
	}

	IEnumerator countOneSecond()
	{

		for (int i = 0; i < timeForRun; i++) {
			yield return new WaitForSeconds (1);
			timePlayed++;
		}

	}
	
	// Update is called once per frame
	void Update () {


		//Debug.Log (timePlayed);



		if (character.IsDead) {
			
			oneHundred.gameObject.SetActive(false);
			tryAgainScreen.SetActive (true);
			timePlayed = 0;
			pointsWhenLost.text = Mathf.Round(TotalPoints).ToString ();
			pointsWhenLost.gameObject.SetActive (true);

			if (wantToTryAgain) {
				
				wantToTryAgain = false;
				character.resetgame ();
				numberOfTries++;
				Time.timeScale = 1f;
				TotalPoints = 0;
			}

		} else {
			setUI ();
		}

		timePlayedText.text = (timePlayed).ToString() + "s";
			
		resetSaveData ();


		
	}


	void setUI()
	{

		if (numberOfTries == 0 && PlayerPrefs.HasKey("HighScore")==false && HighScore == 0) {

			if (grabbedCollect) {
				TotalPoints += amountOfPointsGained;
				HighScore = TotalPoints;
			}

			TotalPoints = TotalPoints + pointChangeSpeed;
			HighScore = TotalPoints;
			PlayerPrefs.SetFloat ("HighScore", HighScore);
			pointsText.text = Mathf.Round (TotalPoints).ToString ();
			hightScoreText.text = ("High Score: ") + Mathf.Round (HighScore).ToString ();

		} else {

			if (grabbedCollect) {
				TotalPoints += amountOfPointsGained;
			}
			

			TotalPoints = TotalPoints + pointChangeSpeed;
			pointsText.text = Mathf.Round (TotalPoints).ToString ();

			if (TotalPoints >= HighScore) {
				HighScore = TotalPoints;
				PlayerPrefs.SetFloat ("HighScore", HighScore);
				hightScoreText.text = ("High Score: ") + Mathf.Round (HighScore).ToString ();
			}
		}
			


	}


	void resetSaveData()
	{
		if (deleteData) {
			PlayerPrefs.DeleteAll ();
		}
			
		
	}

	public void tryAgain()
	{
		wantToTryAgain = true;

	}

	public void exitGame()
	{
		if (character.lifes > 1) {
			character.lifes--;
			afterlog.lifes = character.lifes;
			afterlog.username = userName;
			SceneManager.LoadScene ("AfterLogin");
		} else {
			SceneManager.LoadScene ("MainMenu");
		}



	}

	public void exitGameWithLifes()
	{
		
		afterlog.lifes = character.lifes;
		afterlog.username = userName;
		SceneManager.LoadScene ("AfterLogin");
	}

	public void exitGameWhenDead()
	{
		SceneManager.LoadScene ("MainMenu");
	}





}
