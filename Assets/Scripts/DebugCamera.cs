using UnityEngine;
using System.Collections;

public class DebugCamera : MonoBehaviour {

    public Camera ofCam;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ofCam.enabled = !ofCam.enabled;
        }
	}
}
