using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	CharacterScript character;
	AddObstacle obstacles;

	// Use this for initialization
	void Start () {

		character = GameObject.Find ("Cat").GetComponent<CharacterScript> ();
		//obstacles = GameObject.Find ("Crate").GetComponent<AddObstacle> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (character.IsDead) {

			if (Input.GetButtonDown ("Jump")) {
				character.resetgame ();
				Time.timeScale = 1f;
			}

		}


		
	}
}
