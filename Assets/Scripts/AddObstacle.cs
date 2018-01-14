using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObstacle : MonoBehaviour {

	public GameObject obstacle;
	public GameObject collectible;
	public List<GameObject> obstacleList = new List<GameObject> ();
	public List<GameObject> collectibleList = new List<GameObject> ();
	int timeForRun;
	public float speed = -9f;
	[SerializeField] int boxCount;
	[SerializeField] int collectCount;
	CharacterScript character;
	GameController GC;
	float timeForSpawn = 2.5f;





	// Use this for initialization
	void Start () {

		timeForRun = 999999999;
		boxCount = 0;
		collectCount = 0;
		StartCoroutine(addObjects());
		StartCoroutine(addCollectible());

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

	IEnumerator addCollectible()
	{
		for (int i = 0; i < timeForRun; i++) {

			yield return new WaitForSeconds (Random.Range (5, 15));

			GameObject newCollectible = Instantiate(collectible, new Vector3 (Random.Range(10,12), Random.Range(-1f,0.5f), -1), Quaternion.identity) as GameObject;
			collectibleList.Add (newCollectible);
			collectCount++;
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

		if (collectibleList != null) {

			for (int i = 0; i < collectCount; i++) {
				collectibleList[i].transform.Translate(new Vector2 (speed * Time.deltaTime, 0));

				if (collectibleList[i].transform.position.x < -12) {
					Destroy (collectibleList [i]);
					collectibleList.RemoveAt (i);
					collectCount--;

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

		for (int i = 0; i < collectCount; i++) {
			Destroy (collectibleList [i]);
		}

		collectibleList.Clear ();




		boxCount = 0;
		collectCount = 0;

		timeForSpawn = 2.5f;
		Time.timeScale = 1f;
	}

	void SetDifficulty()
	{
		if (GC.TotalPoints > 100 && GC.TotalPoints < 700) {
			timeForSpawn = 1.8f;
		} else if (GC.TotalPoints > 701 && GC.TotalPoints < 1000) {
			timeForSpawn = Random.Range (0.9f, 2.5f);
		} else if (GC.TotalPoints > 1001 && GC.TotalPoints < 1500) {
			timeForSpawn = Random.Range (0.9f, 2.5f);
		} else if (GC.TotalPoints > 1501 && GC.TotalPoints < 2000) {
			if (!character.IsDead) {
				Time.timeScale = 1.1f;
				timeForSpawn = Random.Range (0.65f, 2.5f);
			}
		} else if (GC.TotalPoints > 2001 && GC.TotalPoints < 3000) {
			if (!character.IsDead) {
				Time.timeScale = 1.2f;
				timeForSpawn = Random.Range (0.65f, 2.5f);
			}
		}
		else if (GC.TotalPoints > 3001 && GC.TotalPoints < 4000) {
			if (!character.IsDead) {
				Time.timeScale = 1.3f;
				timeForSpawn = Random.Range (0.65f, 2.5f);
			}
		}
		else if (GC.TotalPoints > 3001 && GC.TotalPoints < 4000) {
			if (!character.IsDead) {
				Time.timeScale = 1.4f;
				timeForSpawn = Random.Range (0.65f, 2.5f);
			}
		}
		else if (GC.TotalPoints > 4001 && GC.TotalPoints < 5000) {
			if (!character.IsDead) {
				Time.timeScale = 1.5f;
				timeForSpawn = Random.Range (0.65f, 2.5f);
			}
		}
		else if (GC.TotalPoints > 5001 && GC.TotalPoints < 6000) {
			if (!character.IsDead) {
				Time.timeScale = 1.7f;
				timeForSpawn = Random.Range (0.65f, 2.5f);
			}
		}
		else if (GC.TotalPoints > 6001) {
			if (!character.IsDead) {
				Time.timeScale = 1.9f;
				timeForSpawn = Random.Range (0.65f, 2.5f);
			}
		}
	}
}
