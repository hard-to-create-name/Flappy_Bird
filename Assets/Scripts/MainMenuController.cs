using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ViewLeaderboard()
    {
        SceneManager.LoadScene("ShowLeaderboard");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
