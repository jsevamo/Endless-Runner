using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBG : MonoBehaviour {

	public float speed = -7f;
	public GameObject backGround;
	public List<GameObject> bgList = new List<GameObject>();
	int timeForRun;
	int bgCount;


	// Use this for initialization
	void Start () {
		
		timeForRun = 999999999;
		bgCount = 0;

		foreach (GameObject bg in GameObject.FindGameObjectsWithTag("Background")) {
			bgList.Add (bg);
			bgCount++;
		}
		//StartCoroutine(addObjects());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//transform.Translate(new Vector2 (speed * Time.deltaTime, 0));

		GameObject lastbg = bgList [bgCount - 1];

		for (int i = 0; i < bgCount; i++) {
			bgList[i].transform.Translate(new Vector2 (speed * Time.deltaTime, 0));
		}


		if (lastbg.transform.position.x <= 4.6f) {
			GameObject newBG = Instantiate (backGround, new Vector3(31.603f, 0.0089895f,0),  Quaternion.identity) as GameObject;
			bgList.Add (newBG);
			bgCount++;
		}
	}

	IEnumerator addObjects()
	{
		for (int i = 0; i < timeForRun; i++) {
			yield return new WaitForSeconds (1f);
			//Debug.Log (a);
			//a++;

			GameObject lastbg = bgList [bgCount - 1];


			if (lastbg.transform.position.x == 4.6f) {
				GameObject newBG = Instantiate (backGround, new Vector3(31.603f, 0,0),  Quaternion.identity) as GameObject;
				bgList.Add (newBG);
				bgCount++;
			}


		}

	}
}
