using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG1 : MonoBehaviour {

	/*public float fadeSpeed = 1f;    
	public Color fadeColor = new Color (0, 0, 0, 0);

	private Material m_Material;    
	private Color m_Color;

	GameController GC;
	CharacterScript character;


	void Start ()
	{
		GC = GameObject.Find ("GameController").GetComponent<GameController> ();
		character = GameObject.Find ("Player").GetComponent<CharacterScript> ();
		m_Material = GetComponent <Renderer> ().material;
		m_Color = m_Material.color;

	}

	void Update()
	{



	}


	IEnumerator AlphaFade ()
	{
		
		float alpha = 1.0f;


		while (alpha > 0.0f)
		{
			
			alpha -= fadeSpeed * Time.deltaTime;


			m_Material.color = new Color (m_Color.r, m_Color.g, m_Color.b, 1);

			yield return null;
		}
	}



	IEnumerator ColorFade ()
	{
		
		float change = 0.0f;


		while (change < 1.0f)
		{
			
			change += 1 * Time.deltaTime;

			m_Material.color = Color.Lerp (m_Color, fadeColor, change);

			yield return null;
		}
	}
*/


}
