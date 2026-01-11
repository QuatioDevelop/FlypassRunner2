/// <summary>
/// This script use to fade GUI
/// </summary>


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlphaText : MonoBehaviour {
	
	public float speedFade;
	private float count;
	private Image img;
	
	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
		
		//Fade in-out press start
		count += speedFade * Time.deltaTime;
		if (img != null)
			img.color = new Color(0.5f, 0.5f, 0.5f, Mathf.Sin(count) * 0.5f);
	
	}
}
