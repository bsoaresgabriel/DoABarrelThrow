using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class MusicManager : MonoBehaviour {
    public AudioClip mainTheme;
    public AudioClip menuTheme;
    Slider slider;
    // Use this for initialization
    void Start ()
    {
        Audioa4Manager.instance.PlayMusic(menuTheme, 0);
        DontDestroyOnLoad(this);
	}

    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("menuprincipal"))
        {
            if ((GameObject.Find("Canvas").GetComponent<MenuPrincipal>().CreditosON) == false)
            {
                slider = FindObjectOfType<Slider>();
                if (!FindObjectOfType<MenuPrincipal>().loadingScene)
                {
                    slider.value = Audioa4Manager.instance.musicSources[Audioa4Manager.instance.activeMusicSourceIndex].volume;
                }
                slider.onValueChanged.AddListener(delegate { VolumeChange(); });
            }
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game") && FindObjectOfType<StageController>().pauseGameMenu.activeSelf)
        {
            slider = FindObjectOfType<Slider>();
            if (!FindObjectOfType<StageController>().LoadingScene)
            {
                slider.value = Audioa4Manager.instance.musicSources[Audioa4Manager.instance.activeMusicSourceIndex].volume;
            }
            slider.onValueChanged.AddListener(delegate { VolumeChange(); });
        }
        
 
    }

	internal void MusicaNoPlay()
    {
        Audioa4Manager.instance.PlayMusic(mainTheme, 0 );
    }
    internal void MusicaNoMenu()
    {
        Audioa4Manager.instance.PlayMusic(menuTheme, 0.1f);
    }
    void VolumeChange()
    {
        for (int i = 0; i < 2; i++)
        {
            Audioa4Manager.instance.musicSources[Audioa4Manager.instance.activeMusicSourceIndex].volume = slider.value;
            Audioa4Manager.instance.currVolume = Audioa4Manager.instance.musicSources[Audioa4Manager.instance.activeMusicSourceIndex].volume;
            Audioa4Manager.instance.sfxVolumePercent = 10 * slider.value;
        }
    }
}

