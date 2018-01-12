using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObstacle : MonoBehaviour {

	public GameObject obstacle;
	public List<GameObject> obstacleList = new List<GameObject> ();
	int a = 0;
	int timeForRun;
	public float speed = -9f;
	[SerializeField] int boxCount;

	// Use this for initialization
	void Start () {

		timeForRun = 999999999;
		boxCount = 0;
		StartCoroutine(addObjects());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		moveObjects ();
	}

	IEnumerator addObjects()
	{
		for (int i = 0; i < timeForRun; i++) {
			yield return new WaitForSeconds (1f);
			//Debug.Log (a);
			//a++;

			GameObject newObstacle = Instantiate (obstacle, new Vector3 (Random.Range(10,17), -3.75f, -1), Quaternion.identity) as GameObject;
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
}
