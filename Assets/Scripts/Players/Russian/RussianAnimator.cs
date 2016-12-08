using UnityEngine;
using System.Collections;

public class RussianAnimator : MonoBehaviour
{
    internal Animator currAnimator;

    void Start()
    {
        currAnimator = GetComponent<Animator>();
    }
    public void Running(int faceDir, Vector2 velocity, bool ButtonPressed,bool grounded)
    {
        currAnimator.SetFloat("Run", velocity.x);
        currAnimator.SetFloat("FaceDir", faceDir);
        currAnimator.SetBool("Pressed", ButtonPressed);
        currAnimator.SetBool("Grounded", grounded);
        if (grounded)
        {
            currAnimator.SetBool("Jump", false);
        }
    }
    public void Jump(bool Jump, bool grounded)
    {
        currAnimator.SetBool("Jump", Jump);
        currAnimator.SetBool("Grounded", grounded);
    }
    public void Barrel(bool barrel)
    {
        currAnimator.SetBool("HasBarrel", barrel);
    }
    public void Throws(int direction,bool hasbarrel,int facedir)
    {
        if (currAnimator.GetBool("FirstThrow") == true)
        {
            currAnimator.SetBool("FirstThrow", false);
        }
        currAnimator.SetFloat("DirecaoThrow", direction);
        currAnimator.SetBool("HasBarrel", hasbarrel);
        currAnimator.SetFloat("FaceDir", facedir);
        StartCoroutine(ThrownTime());
    }
    private IEnumerator ThrownTime()
    {
        yield return new WaitForSeconds(.25f);
        currAnimator.SetBool("Thrown", true);
        currAnimator.SetBool("HasBarrel", BarrelTypes.instance.collectableInfo.hasRegularBarrel);
        yield break;
    }
    public void resetThrown()
    {
        currAnimator.SetBool("Thrown", false);
    }
    internal void StartDeathProcedure()
    {
        currAnimator.SetBool("Death", true);
        StartCoroutine(DeathBoolTime());
    }
    private IEnumerator DeathBoolTime()
    {
        yield return new WaitForSeconds(.2f);
        currAnimator.SetBool("Death", false);
        yield break;
;    }
}
