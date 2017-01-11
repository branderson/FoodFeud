using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    private int score;

	// Use this for initialization
	void Start ()
	{
	    score = LevelController.instance.Score;
        Destroy(LevelController.instance.gameObject);
	    GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>().text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Start"))
	    {
	        Retry();
	    }
	}

    public void Retry()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
