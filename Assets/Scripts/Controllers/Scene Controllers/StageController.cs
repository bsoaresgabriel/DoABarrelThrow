using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{

    //public Button Submit;
    public GameObject gameOverMenu,pauseGameMenu;
    public Image MuteButton;
    public SpriteRenderer muted, UnMuted;
    internal bool LoadingScene = false;
    public bool mutado;
    public GameObject loadMovObject;


    internal void DeathProcedure(int DeathCause,GameObject Char)
    {
        gameOverMenu.SetActive(true);
    }
    void Start()
    {
        mutado = Audioa4Manager.instance.musicSources[Audioa4Manager.instance.activeMusicSourceIndex].mute;
    }
    void Update()
    {
        if (!gameOverMenu.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseGameMenu.activeInHierarchy)
                {
                    Time.timeScale = 1;
                    pauseGameMenu.SetActive(false);
                }
                else
                {
                    Time.timeScale = 0;
                    pauseGameMenu.SetActive(true);
                }
            }
        }
            if (mutado)
            {
                MuteButton.sprite = muted.sprite;
            }
            else
            {
                MuteButton.sprite = UnMuted.sprite;
            }
        
    }
        public void mute()
    {
        
        if (Audioa4Manager.instance.musicSources[Audioa4Manager.instance.activeMusicSourceIndex].mute)
        {
            Audioa4Manager.instance.musicSources[Audioa4Manager.instance.activeMusicSourceIndex].mute = false;
            mutado = false;
            Audioa4Manager.instance.sfxVolumePercent = 8;
        }
        else
        {
            Audioa4Manager.instance.musicSources[Audioa4Manager.instance.activeMusicSourceIndex].mute = true;
            mutado = true;
            Audioa4Manager.instance.sfxVolumePercent = 0;
        }

    }
    IEnumerator LoadMov()
    {
        SceneManager.LoadSceneAsync("menuprincipal").allowSceneActivation = false;
        //yield return new WaitForSeconds(((MovieTexture)loadMovObject.GetComponent<RawImage>().texture).duration);
        //((MovieTexture)loadMovObject.GetComponent<RawImage>().texture).Stop();
        SceneManager.LoadScene("menuprincipal");
        yield break;
    }
    public void SaindoDaCena()
    {
        //loadMovObject.SetActive(true);
        
        //((MovieTexture)loadMovObject.GetComponent<RawImage>().texture).Play();
        StartCoroutine(LoadMov());
    }
}
