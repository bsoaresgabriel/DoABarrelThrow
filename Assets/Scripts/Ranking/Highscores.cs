using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Highscores : MonoBehaviour{
	public Text[] Rank;
	public Text[] Name;
	public Text[] Score;
	//mostrar novo highscore 	LocalScore.instance.localScore.playername     LocalScore.instance.localScore.score.ToString()
	const string PrivateCode = "DtJGvuodZEmlRfHRBz5KfwlAS-qJI5DUiw1iD8M7I3hQ";
	const string PublicCode = "584310fb8af6031350c7c87f";
	const string webURL = "http://dreamlo.com/lb/";


	public void Insert(){
		print ("FIINAL: " + LocalScore.instance.localScore.score);
		AddNewHighscore (LocalScore.instance.localScore.playername, decimal.ToInt32((LocalScore.instance.localScore.score)));
	}


	public Highscore[] highscoresList;

	public void AddNewHighscore(string username, float score){
		StartCoroutine(UploadNewHighscore(username, score));	
	}

	IEnumerator UploadNewHighscore (string username, float score){
		WWW www = new WWW (webURL + PrivateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
		yield return www;

		if ((string.IsNullOrEmpty) (www.error))
			print ("Upload Sucessful");
		else {
			print ("Error Uploading" + www.error);
		}
	}

	public void startShowing(){
		this.gameObject.SetActive(true);
		FindObjectOfType<MenuPrincipal>().RankingButton.gameObject.SetActive(false);
		StartCoroutine("DownloadhighscoresFromDatabase");
	}

	IEnumerator DownloadhighscoresFromDatabase (){
		WWW www = new WWW (webURL + PublicCode + "/pipe/0/5");
		yield return www;

		if ((string.IsNullOrEmpty)(www.error))
			FormatHighscores(www.text);
		else {
			print ("Error Downloading" + www.error);
		}
	}

	void FormatHighscores(string textStream){
		string[] entries = textStream.Split (new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];

		for (int i = 0; i < 6; i++) {
			string[] entryInfo = entries [i].Split (new char[] { '|' });
			string username = entryInfo [0];
			float score = float.Parse(entryInfo [1]);
			highscoresList[i] = new Highscore (username, score);
			Name[i].text = highscoresList[i].username;
			Score[i].text = (highscoresList[i].score).ToString();
			Rank[i].text = (i+1).ToString();
			print (highscoresList[i].username + "__" + highscoresList[i].score);
		}
	}
}

public struct Highscore{
	public string username;
	public float score;

	public Highscore(string _username, float _score){
		username = _username;
		score = _score;
	}
}

