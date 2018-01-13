using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	CharacterScript character;
	public Text pointsText;
	public Text hightScoreText;
	[HideInInspector] public float TotalPoints;
	[HideInInspector] public float HighScore;
	int numberOfTries;
	public float pointChangeSpeed = 0.3f;




	// Use this for initialization
	void Start () {

		character = GameObject.Find ("Cat").GetComponent<CharacterScript> ();
		TotalPoints = 0;
		HighScore = 0;
		pointsText.text = 0.ToString();
		numberOfTries = 0;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (character.IsDead) {

			if (Input.GetButtonDown ("Jump")) {
				character.resetgame ();
				numberOfTries++;
				Time.timeScale = 1f;
				TotalPoints = 0;

			}

		} else {
			setPointsToUI ();
		}
			

		
	}


	void setPointsToUI()
	{

		if (numberOfTries == 0) {
			TotalPoints = TotalPoints + pointChangeSpeed;
			HighScore = HighScore + pointChangeSpeed;
			pointsText.text = Mathf.Round (TotalPoints).ToString ();
			hightScoreText.text = ("High Score: ") + Mathf.Round (HighScore).ToString ();

		} else {
			TotalPoints = TotalPoints + pointChangeSpeed;
			pointsText.text = Mathf.Round (TotalPoints).ToString ();

			if (TotalPoints >= HighScore) {
				HighScore = HighScore + pointChangeSpeed;
				hightScoreText.text = ("High Score: ") + Mathf.Round (HighScore).ToString ();
			}
		}








	}
}
