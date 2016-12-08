using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public void YesButton()
    {
        try
        {
            if (FindObjectOfType<StageController>().pauseGameMenu.activeInHierarchy)
            {
                FindObjectOfType<StageController>().pauseGameMenu.SetActive(false);
                Time.timeScale = 1;
            }
            Audioa4Manager.instance.PlaySound2D("Play");
            SceneManager.LoadScene("Game");
            GetComponent<RussianPlayer>().enabled = true;

        }
        catch
        {

        }
        }
    public void NoButton()
    {
        Audioa4Manager.instance.PlaySound2D("Quit");
        FindObjectOfType<MusicManager>().MusicaNoMenu();
        FindObjectOfType<StageController>().LoadingScene = true;
        FindObjectOfType<StageController>().SaindoDaCena();
        Time.timeScale = 1;
    }
}
