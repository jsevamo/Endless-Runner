using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

	public float speed = 0.1f;
	Renderer rend;


	// Use this for initialization
	void Start () {

		rend = GetComponent<Renderer> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector2 offset = new Vector2 (Time.time * speed, 0);
		rend.material.mainTextureOffset = offset;
		
	}


}
