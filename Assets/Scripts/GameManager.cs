using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{// 游戏的三个状态
    public static int GAMESTATE_MENU = 0;//游戏菜单状态
    public static int GAMESTATE_PLAYING = 1;//游戏中状态 ... 
    public static int GAMESTATE_END = 2;//游戏结束状态
    public Transform firstBg;
    public int score = 0;
    public int GameState = GAMESTATE_MENU;//初始状态
    public static GameManager _intance;
    private GameObject bird;
    public Text scoreUIText;
    void Awake()
    {
        _intance = this;
        bird = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (GameState == GameManager.GAMESTATE_MENU)
        {
            if (Input.GetMouseButtonDown(0))//判断是否点击
            {
                GameState = GAMESTATE_PLAYING;//鼠标点击则状态转换为游戏中状态
                bird.SendMessage("getLife");
            }
        }
        if(GameState == GameManager.GAMESTATE_END)//游戏结束状态则显示score窗口
        {
            int finalScore = score;
            PlayerPrefs.SetInt("finalScore", finalScore);
            SceneManager.LoadScene("ShowLeaderboard");
        }
    }
}
