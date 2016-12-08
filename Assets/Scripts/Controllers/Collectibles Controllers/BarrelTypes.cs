using UnityEngine;
using System.Collections;

public class BarrelTypes : MonoBehaviour
{
    public CollectibleInfo collectableInfo;
    public static BarrelTypes instance;
    public Shield shieldToBeInstanced;
    public Shield shield;
    public float scorePerBarrel;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        collectableInfo.shieldTime = 4.5f;
        collectableInfo.doubleScoreTime = 6f;
    }
    public void collectedSomething(GameObject barrel,Vector3 velocity,Transform parentTransform)
    {
        switch (barrel.tag)
        {


            case ("Barril"):
                Audioa4Manager.instance.PlaySound2D("PickBarrel");  
                collectableInfo.hasRegularBarrel = true;
                if (collectableInfo.hasDoubleScore)
                {
                    LocalScore.instance.addScore(scorePerBarrel * 2);
                }
                else
                {
                    LocalScore.instance.addScore(scorePerBarrel);
                }
                FindObjectOfType<RussianAnimator>().Barrel(collectableInfo.hasRegularBarrel);
            if (collectableInfo.regularBarrelCount < 2)
            {
                collectableInfo.regularBarrelCount++;
            }
                break;
            case ("Escudo"):
               shield = Instantiate(shieldToBeInstanced, parentTransform.position, parentTransform.transform.rotation) as Shield;
                shield.transform.parent = parentTransform;
                Audioa4Manager.instance.PlaySound2D("ShieldBarrel");
                if (collectableInfo.hasDoubleScore)
                {
                    LocalScore.instance.addScore(scorePerBarrel * 2);
                }
                else
                {
                    LocalScore.instance.addScore(scorePerBarrel);
                }
                StartCoroutine(ShieldRoutine(velocity));
                break;
            case ("Double"):
                Audioa4Manager.instance.PlaySound2D("DoubleBarrel");
                if (collectableInfo.hasDoubleScore)
                {
                    LocalScore.instance.addScore(scorePerBarrel * 2);
                }
                else
                {
                    LocalScore.instance.addScore(scorePerBarrel);
                }
                StartCoroutine(DoubleScoreRoutine());
                break;
        }
    }
    public struct CollectibleInfo
    {
        public bool hasShield;
        public bool hasDoubleScore;
        public float shieldTime;
        public bool hasRegularBarrel;
        public int regularBarrelCount;
        public float doubleScoreTime;
    }
    public IEnumerator ShieldRoutine(Vector3 velocity)
    {
        collectableInfo.hasShield = true;
        StartCoroutine(ShieldCollisionsSubRoutine(velocity));
        yield return new WaitForSeconds(collectableInfo.shieldTime);
        collectableInfo.hasShield = false;
        Destroy(shield.gameObject);
        yield break;
    }
    public IEnumerator ShieldCollisionsSubRoutine(Vector3 velocity)
    {
        while (collectableInfo.hasShield)
        {
            shield.DetectHorizontal(velocity);
            shield.DetectVertical(velocity);
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
    public IEnumerator DoubleScoreRoutine()
    {
        collectableInfo.hasDoubleScore = true;
        yield return new WaitForSeconds(collectableInfo.doubleScoreTime);
        collectableInfo.hasDoubleScore = false;
        yield break;
    }
}
