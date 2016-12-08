using UnityEngine;
using System.Collections;

public class Audioa4Manager : MonoBehaviour {

    public enum AudioChannel { Master, Sfx, Music };

    float masterVolumePercent = .2f;
    public float sfxVolumePercent = 8;
    float musicVolumePercent = 1f;
    public float currVolume = 1;

    AudioSource sfx2DSource;
    public AudioSource[] musicSources;
    public int activeMusicSourceIndex;
    internal bool firstCall = true;

    public static Audioa4Manager instance;

    Transform audioListener;
    Transform playerT;

    SoundLibrary library;


    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            library = GetComponent<SoundLibrary>();

            musicSources = new AudioSource[2];
            for (int i = 0; i < 2; i++)
            {
                GameObject newMusicSource = new GameObject("Music source " + (i + 1));
                musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = transform;
            }

            GameObject newSfx2Dsource = new GameObject("2D sfx source");
            sfx2DSource = newSfx2Dsource.AddComponent<AudioSource>();
            newSfx2Dsource.transform.parent = transform;

            audioListener = FindObjectOfType<AudioListener>().transform;
            //playerT = FindObjectOfType<RussianPlayer>().transform;

        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources [activeMusicSourceIndex].clip = clip;
        musicSources [activeMusicSourceIndex].Play();
        musicSources[activeMusicSourceIndex].loop = true;

        StartCoroutine(AnimateMusicCrossfade(fadeDuration));
    }
    void Update()
    {
        if (playerT != null)
        {
            audioListener.position = playerT.position;
        }
    }

    public void PlaySound (AudioClip clip, Vector3 pos)
    {
        if (!clip)
        {
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
        }
    }

    public void PlaySound(string soundName, Vector3 pos)
    {
        PlaySound(library.GetClipFromName(soundName), pos);
    }

    public void PlaySound2D(string soundName)
    {
        sfx2DSource.PlayOneShot(library.GetClipFromName(soundName), sfxVolumePercent * masterVolumePercent);
    }

    IEnumerator AnimateMusicCrossfade(float duration)
    {
        float percent = 0;

        while (percent < 1)
        {if (firstCall)
            {
                currVolume = 1;
                firstCall = false;
                percent = Time.deltaTime * 1 / duration;
            }
            else
            {
                percent += Time.deltaTime * 1 / duration;
            }
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0,currVolume, percent);
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(currVolume, 0, percent);
            if (musicSources[1-activeMusicSourceIndex].mute)
            {
                musicSources[activeMusicSourceIndex].mute = true;
            }
            else
            {
                musicSources[activeMusicSourceIndex].mute = false;
            }
            yield return null;
        }
    }

}
