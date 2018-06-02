using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class StandardHighScores : MonoBehaviour
{
    public Leaderboard _scoreManager;
    private string nameInput = "";
    private bool newHighScore;
    private int finalGameScore;
    public Text leaderboardUIText_rank;
    public Text leaderboardUIText_name;
    public Text leaderboardUIText_score;
    public InputField playerNameInputField;
    public GameObject enterNamePanel;
    public bool showResetButton;
    public GameObject resetButton;
    // --------------------------------------------------------------------
    public void GotoMainMenu()
    {
        // 转到菜单场景 
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayAgain()
    {
        //转到游戏场景
        SceneManager.LoadScene("Game");
    }
    // ---------------------------------------------------------------------
    void Start()
    {
        // 重置按钮
        if (showResetButton)
        {
            resetButton.SetActive(true);
        } else
        {
            resetButton.SetActive(false);
        }
        //首先确定排行榜已经初始化
        _scoreManager.SetUpScores();
        // 调用本地FinalScore
        finalGameScore = PlayerPrefs.GetInt("finalScore");
        // 判断是否刷新榜单，是否需要输入名字
        newHighScore = _scoreManager.DidGetHighScore(finalGameScore);
        Debug.Log(">StandardHighScores.cs>This is a high score? " + newHighScore);
        UpdateUIText();
        // 检查是否需要弹出输入名字的框
        // 并确定分数不为0
        if(newHighScore && finalGameScore!=0)
        {
            ShowEnterNamePanel();
        } else
        {
            HideEnterNamePanel();
        }
    }
    public void ShowEnterNamePanel()
    {
        // 设置名字最大长度
        playerNameInputField.characterLimit = _scoreManager.maxNameLength;  
        enterNamePanel.SetActive(true);
        // 无需点击文本框即可输入内容
        playerNameInputField.Select();
    }
    public void HideEnterNamePanel()
    {
        enterNamePanel.SetActive(false);
    }
    public void UpdateUIText()
    {
        Debug.Log(">StandardHighScores.cs>Updating UI text for "+_scoreManager.numberOfScores+" scores.");
        //初始化文本框
        leaderboardUIText_rank.text = "";
        leaderboardUIText_name.text = "";
        leaderboardUIText_score.text = "";
        for (var i = 1; i <= _scoreManager.numberOfScores; i++)
        {
            // 排名
            leaderboardUIText_rank.text = leaderboardUIText_rank.text + i.ToString() + ".\n";
            // 名字
            leaderboardUIText_name.text = leaderboardUIText_name.text + _scoreManager.GetNameAt(i) + "\n";
            // 分数
            leaderboardUIText_score.text = leaderboardUIText_score.text + _scoreManager.GetScoreAt(i).ToString() + "\n";
        }
    }
    public void NameSubmit()
    {
        // 弹出姓名框
        HideEnterNamePanel();
        // 未输入名字则显示无名氏
        if (playerNameInputField.text == "")
            playerNameInputField.text = "无名氏";
        _scoreManager.SubmitLocalScore(playerNameInputField.text, finalGameScore);
        UpdateUIText();
        PlayerPrefs.SetInt("finalScore", 0);
    }
    public void ResetLeaderboard()
    {
        //重置排行榜
        _scoreManager.ResetAllScores();
        UpdateUIText();
    }
}