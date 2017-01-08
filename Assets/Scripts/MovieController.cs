using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MovieController : MonoBehaviour {

	// Use this for initialization
	void Start () {
//        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Start"))
	    {
	        SceneManager.LoadScene("LevelScene");
	    }
	}
}
