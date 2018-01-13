using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObstacle : MonoBehaviour {

	public GameObject obstacle;
	public List<GameObject> obstacleList = new List<GameObject> ();
	int timeForRun;
	public float speed = -9f;
	[SerializeField] int boxCount;
	CharacterScript character;
	GameController GC;
	float timeForSpawn = 2f;



	// Use this for initialization
	void Start () {

		timeForRun = 999999999;
		boxCount = 0;
		StartCoroutine(addObjects());

		character = GameObject.Find ("Cat").GetComponent<CharacterScript> ();
		GC = GameObject.Find ("GameController").GetComponent<GameController> ();

	}


	
	// Update is called once per frame
	void FixedUpdate () {
		moveObjects ();
	}

	void Update()
	{
		if (character.IsDead) {
			StopCoroutine("addObjects");

		}

		if (character.hasReseted) {
			resetgame ();
			character.hasReseted = false;

		}

		SetDifficulty ();
		Debug.Log (timeForSpawn);
	}

	IEnumerator addObjects()
	{
		for (int i = 0; i < timeForRun; i++) {
			
			yield return new WaitForSeconds (timeForSpawn);

			GameObject newObstacle = Instantiate (obstacle, new Vector3 (Random.Range(10,12), -3.75f, -1), Quaternion.identity) as GameObject;
			obstacleList.Add (newObstacle);
			boxCount++;
		}

	}


	void moveObjects()
	{
		if (obstacleList != null) {
			
			for (int i = 0; i < boxCount; i++) {
				obstacleList[i].transform.Translate(new Vector2 (speed * Time.deltaTime, 0));

				if (obstacleList[i].transform.position.x < -12) {
					Destroy (obstacleList [i]);
					obstacleList.RemoveAt (i);
					boxCount--;

				}
			}
		}


	}

	public void resetgame()
	{
		
		for (int i = 0; i < boxCount; i++) {
			Destroy (obstacleList [i]);
		}

		obstacleList.Clear ();
		boxCount = 0;

		timeForSpawn = 2f;
		Time.timeScale = 1f;
	}

	void SetDifficulty()
	{
		if (GC.TotalPoints > 100 && GC.TotalPoints < 700) {
			timeForSpawn = 1.5f;
		} else if (GC.TotalPoints > 701 && GC.TotalPoints < 1000) {
			timeForSpawn = 1.2f;
		} else if (GC.TotalPoints > 1001 && GC.TotalPoints < 1500) {
			timeForSpawn = 0.9f;
		} else if (GC.TotalPoints > 1501 && GC.TotalPoints < 2000) {
			if (!character.IsDead) {
				Time.timeScale = 1.1f;
			}
		} else if (GC.TotalPoints > 2001 && GC.TotalPoints < 3000) {
			if (!character.IsDead) {
				Time.timeScale = 1.2f;
				timeForSpawn = 0.85f;
			}
		}
		else if (GC.TotalPoints > 3001 && GC.TotalPoints < 4000) {
			if (!character.IsDead) {
				Time.timeScale = 1.3f;
			}
		}
		else if (GC.TotalPoints > 3001 && GC.TotalPoints < 4000) {
			if (!character.IsDead) {
				Time.timeScale = 1.4f;
			}
		}
		else if (GC.TotalPoints > 4001 && GC.TotalPoints < 5000) {
			if (!character.IsDead) {
				Time.timeScale = 1.5f;
			}
		}
	}
}
