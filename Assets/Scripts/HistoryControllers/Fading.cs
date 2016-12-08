using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(loadNextScene());
    }
    IEnumerator loadNextScene()
    {
        SceneManager.LoadSceneAsync("menuprincipal").allowSceneActivation = false;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("menuprincipal");
    }
}
