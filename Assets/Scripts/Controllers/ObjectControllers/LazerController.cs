using UnityEngine;
using System.Collections;

public class LazerController : MonoBehaviour
{
    public BeamController theBeam;
	// Use this for initialization
	void Start ()
    {
        theBeam.StartLazer();
	}
    public void StartLazerLoop()
    {
        StartCoroutine(LazerLoop());
    }
    public IEnumerator LazerLoop()
    {
        yield return new WaitForSeconds(3f);
        theBeam.StartLazer();
        yield break;
    }
	

}
