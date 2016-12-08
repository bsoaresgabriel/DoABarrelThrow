using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LocalScore : MonoBehaviour
{
    public Score localScore;
    public Text scoreText;
    public static LocalScore instance;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void AddUserName(string userName)
    {
        localScore.playername = userName;
        //print(localScore.playername);
    }
    public void addScore(float value)
    {
            localScore.score += decimal.Parse(value.ToString());
        if (localScore.score > 0)
        {
            scoreText.text = ("SCORE:") + decimal.Round(localScore.score, 2);
        }
    }
    public float CheckUser(string playerName)
    {
        if (PlayerPrefs.HasKey(playerName))
        {
            return PlayerPrefs.GetFloat(playerName);
        }
        return 0;
    }
    /*public void SaveLocalScore()
    {
        PlayerPrefs.SetFloat(localScore.playername, float.Parse(localScore.score.ToString()));
        PlayerPrefs.Save();
    }*/
}
public struct Score
{
    public string playername;
    public decimal score;
}
