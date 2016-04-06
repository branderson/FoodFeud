using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Start"))
	    {
//	        SceneManager.LoadScene("MovieScene");
	        SceneManager.LoadScene("LevelScene");
	    }
	}
}
