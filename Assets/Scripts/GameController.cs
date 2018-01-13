using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	CharacterScript character;
	public Text pointsText;
	[HideInInspector] public float TotalPoints;
	float framesThatHavePassed;



	// Use this for initialization
	void Start () {

		character = GameObject.Find ("Cat").GetComponent<CharacterScript> ();
		pointsText.text = 0.ToString();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (character.IsDead) {

			if (Input.GetButtonDown ("Jump")) {
				character.resetgame ();
				Time.timeScale = 1f;
				TotalPoints = 0;
			}

		} else {
			setPointsToUI ();
		}




		
	}


	void setPointsToUI()
	{
		TotalPoints = TotalPoints+0.3f;
		pointsText.text = Mathf.Round(TotalPoints).ToString();
	}
}
