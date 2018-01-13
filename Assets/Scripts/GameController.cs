using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	CharacterScript character;
	public Text pointsText;
	public Text hightScoreText;
	public Text lifeCountText;
	public Text losingText;
	public Button tryAgainButton;
	[HideInInspector] public float TotalPoints;
	[HideInInspector] public float HighScore;
	int numberOfTries;
	public float pointChangeSpeed = 0.3f;
	public bool deleteData = false;
	bool wantToTryAgain;




	// Use this for initialization
	void Start () {

		character = GameObject.Find ("Cat").GetComponent<CharacterScript> ();
		TotalPoints = 0;

		if (PlayerPrefs.HasKey("HighScore")) {

			HighScore = PlayerPrefs.GetFloat ("HighScore");
			hightScoreText.text = ("High Score: ") + Mathf.Round (HighScore).ToString ();

		} else if(deleteData == true) {
			HighScore = 0;
		}


		pointsText.text = 0.ToString();
		numberOfTries = 0;

		wantToTryAgain = false;
		
	}
	
	// Update is called once per frame
	void Update () {



		if (character.IsDead) {

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
			
		resetSaveData ();
		
	}


	void setUI()
	{

		if (numberOfTries == 0 && PlayerPrefs.HasKey("HighScore")==false && HighScore == 0) {
			TotalPoints = TotalPoints + pointChangeSpeed;
			HighScore = HighScore + pointChangeSpeed;
			PlayerPrefs.SetFloat ("HighScore", HighScore);
			pointsText.text = Mathf.Round (TotalPoints).ToString ();
			hightScoreText.text = ("High Score: ") + Mathf.Round (HighScore).ToString ();

		} else {
			TotalPoints = TotalPoints + pointChangeSpeed;
			pointsText.text = Mathf.Round (TotalPoints).ToString ();

			if (TotalPoints >= HighScore) {
				HighScore = HighScore + pointChangeSpeed;
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


}
