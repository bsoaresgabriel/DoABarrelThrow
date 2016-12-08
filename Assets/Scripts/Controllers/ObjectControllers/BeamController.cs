using UnityEngine;
using System.Collections;

public class BeamController : MonoBehaviour
{
    public bool firstCycle = false;
    public float lazerTime;
    public void StartLazer()
    {
        gameObject.SetActive(true);
    }
    public void FireLazor()
    {
        Animator Lazernimator = GetComponent<Animator>();
        Lazernimator.SetBool("LazerFiring", true);
        StartCoroutine(LazerFiring());
    }
    IEnumerator LazerFiring()
    {
        yield return new WaitForSeconds(lazerTime);
        Animator Lazernimator = GetComponent<Animator>();
        Lazernimator.SetBool("LazerCooling", true);
        Lazernimator.SetBool("LazerFiring", false);
        yield break;
    }
    public void StopLazer()
    {
        FindObjectOfType<LazerController>().StartLazerLoop();
        gameObject.SetActive(false);
    }
}
