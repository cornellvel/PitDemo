using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Valve.VR;

public class PlaySound : MonoBehaviour {

    public AudioSource fallingsound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fallingsound.Play();
        }
	}
}
