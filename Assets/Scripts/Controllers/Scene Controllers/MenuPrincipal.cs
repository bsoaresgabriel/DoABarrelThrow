using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class MenuPrincipal : MonoBehaviour
{
    public Button PlayButton, HelpButton, QuitButton, AudioButton, CreditsButton,RankingButton;
    public GameObject loadMovObject;
    public string GameSceneName = "Game";
    public Image MuteButton;
    public SpriteRenderer muted,UnMuted;
    public GameObject Help;
    public GameObject Credits;
    public GameObject Ranking;
    public bool mutado = false;
    public GameObject slider;
    public GameObject logo;
    public bool CreditosON;
    public bool loadingScene;


    // Use this for initialization
    void Start()
    {
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Cursor.visible = true;
        CreditosON = false;
        loadingScene = false;
        mutado = Audioa4Manager.instance.musicSources[Audioa4Manager.instance.activeMusicSourceIndex].mute;
    }


    // Update is called once per frame
    void Update()
    {

        if (Credits.activeInHierarchy == true)
        {
            CreditosON = true;
            PlayButton.gameObject.SetActive(false);
            HelpButton.gameObject.SetActive(false);
            QuitButton.gameObject.SetActive(false);
            AudioButton.gameObject.SetActive(false);
            CreditsButton.gameObject.SetActive(false);
            RankingButton.gameObject.SetActive(false);
            slider.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Help.activeInHierarchy)
            {
                Help.SetActive(false);
                logo.SetActive(true);
            }
            if (Credits.activeInHierarchy == true)
            {
                CreditosON = false;
                Credits.SetActive(false);
                PlayButton.gameObject.SetActive(true);
                HelpButton.gameObject.SetActive(true);
                QuitButton.gameObject.SetActive(true);
                AudioButton.gameObject.SetActive(true);
                CreditsButton.gameObject.SetActive(true);
                RankingButton.gameObject.SetActive(true);
                slider.SetActive(true);
                Credits.transform.position = new Vector3(0, -35f, 0);
            }
            if (Ranking.activeInHierarchy == true)
            {
                Ranking.SetActive(false);
                RankingButton.gameObject.SetActive(true);
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

    public void Jogar()
    {
        Audioa4Manager.instance.PlaySound2D("Play");
        //loadMovObject.SetActive(true);
        //Handheld.PlayFullScreenMovie("load_ani", Color.black, FullScreenMovieControlMode.Hidden);
        //((MovieTexture)loadMovObject.GetComponent<RawImage>().texture).Play();
        StartCoroutine(movPlaying());
        FindObjectOfType<MusicManager>().MusicaNoPlay();
        loadingScene = true;
    }

    public void Sair()
    {
        Audioa4Manager.instance.PlaySound2D("Quit");
        Application.Quit();
    }

    public void help()
    {
        Audioa4Manager.instance.PlaySound2D("Help");
        GameObject.FindGameObjectWithTag("Logo").SetActive(false);
        Help.SetActive(true);
    }

    public void credits()
    {
        Credits.SetActive(true);
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
    IEnumerator movPlaying()
    {
        SceneManager.LoadSceneAsync("Game").allowSceneActivation = false;
        //yield return new WaitForSeconds((loadMovObject.GetComponent<RawImage>().texture as MovieTexture).duration);
        //((MovieTexture)loadMovObject.GetComponent<RawImage>().texture).Stop();
        SceneManager.LoadScene("Game");
        yield break;
    }
}

//((MovieTexture)loadMovObject.GetComponent<RawImage>().texture).duration
