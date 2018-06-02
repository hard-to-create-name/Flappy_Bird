using UnityEngine;
using System.Collections;
public class Leaderboard : MonoBehaviour
{
    public int maxNameLength = 10; // 名字最大长度
    public int numberOfScores = 10; // 排行榜大小
    private bool doneSetup;
    private int rank;
    private string[] names;  //玩家姓名
    private int[] scores;    //分数
    private int scoreBoardIndex;
    // -----------------------------------------------------------------------------------------
    public void SetUpScores()
    {
        scoreBoardIndex = 0;
        CheckScores();
    }
    // -----------------------------------------------------------------------------------------
    public void SetUpScores( int boardIndex )
    {
        scoreBoardIndex = boardIndex;
        CheckScores();
    }
    // -----------------------------------------------------------------------------------------
    public void CheckScores()
    {
        // 检查排行榜是否存在
        if (PlayerPrefs.GetInt("hasLeaderboard" + scoreBoardIndex) != 2)
        {
            BuildDefaultTable();
            PlayerPrefs.SetInt("hasLeaderboard" + scoreBoardIndex, 2);
        }
        else {
            LoadScores();
        }
    }
    // -----------------------------------------------------------------------------------------
    public void ResetAllScores()
    {
        BuildDefaultTable();
    }
    // -----------------------------------------------------------------------------------------
    public void BuildDefaultTable()
    {
        Debug.Log(">Laderboard.cs>Building default score table..");
        for (var i = 0; i < numberOfScores; i++)
        {
            PlayerPrefs.SetString(scoreBoardIndex + "leaderBoardName" + i, "PsychicParrot");
            PlayerPrefs.SetInt(scoreBoardIndex + "leaderBoardScore" + i, 0);
        }
        names = new string[numberOfScores];
        scores = new int[numberOfScores];
        LoadScores();
        doneSetup = true;
    }
    // -----------------------------------------------------------------------------------------

    public string GetNameAt( int index )
	{
		if(!doneSetup){
			Debug.LogError("ERROR: Leaderboard not set up and something is calling getFormattedStringAt.");
		}
		return names[index - 1];
	}
	// -----------------------------------------------------------------------------------------	
	public int GetScoreAt( int index )
	{
		if(!doneSetup){
			Debug.LogError("ERROR: Leaderboard not set up and something is calling getFormattedStringAt.");
		}
		return scores[index - 1];
	}
	// -----------------------------------------------------------------------------------------	
	public bool DidGetHighScore( int aScore )
	{
        rank = GetHighScoreRank(aScore);
        if (rank<numberOfScores)
		{
			return true;
		}		
		return false;
	}	
	public int GetHighScoreRank( int aScore )
	{
        rank = numberOfScores+1;
        for ( int i =(numberOfScores-1); i>-1; --i){
			if(scores[i]<aScore){
				rank=i;
			}
		}	
		if(rank<numberOfScores)
		{
			return 0;
		}		
		return (rank+1);
	}	
	// -----------------------------------------------------------------------------------------	
	public void SubmitLocalScore( string playerName , int theScore )//将获取的分数提交至排行榜
    {
        if (!doneSetup)
        {
            Debug.LogError("ERROR: Leaderboard not set up and something is calling submitLocalScore.");
        }
        if (playerName == "")
            playerName = "Anon";
        if (playerName.Length > maxNameLength)
        {
            playerName = playerName.Substring(0, maxNameLength);
        }
        rank = numberOfScores + 1;
        for ( int r = (numberOfScores - 1); r > -1; --r){
            if (scores[r] < theScore)
            {
                rank = r;
            }
        }
        int[] scoresCopy = new int[numberOfScores];
        string[] namesCopy = new string[numberOfScores];

        for (var i = (numberOfScores - 1); i > -1; --i)
        {
            if (i > rank)
            {
                scoresCopy[i] = scores[i - 1];
                namesCopy[i] = names[i - 1];
            }
            else if (i == rank)
            {
                scoresCopy[i] = theScore;
                namesCopy[i] = playerName;
            }
            else {
                scoresCopy[i] = scores[i];
                namesCopy[i] = names[i];
            }
        }
        for ( int a = 0; a < numberOfScores; ++a){
            names[a] = namesCopy[a];
            scores[a] = scoresCopy[a];
        }
        SaveScores();
    }
    // -----------------------------------------------------------------------------------------
    public void SaveScores()  //保存历史分数
    {
        // save our scores out to player prefs
        for (var i = 0; i < numberOfScores; i++)
        {
            PlayerPrefs.SetString(scoreBoardIndex + "leaderBoardName" + i, names[i]);
            PlayerPrefs.SetInt(scoreBoardIndex + "leaderBoardScore" + i, scores[i]);
        }
    }
    public void LoadScores() //载入本地分数
    {
        names = new string[numberOfScores];
        scores = new int[numberOfScores];
        for (var i = 0; i < numberOfScores; i++)
        {
            names[i] = PlayerPrefs.GetString(scoreBoardIndex + "leaderBoardName" + i);
            scores[i] = PlayerPrefs.GetInt(scoreBoardIndex + "leaderBoardScore" + i);
        }
        doneSetup = true;
    }

}