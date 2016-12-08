using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour
{
    public GameObject barrilPrefab;
    public GameObject barrilDoublePrefab;
    public GameObject barrilEscudoPrefab;
    public GameObject spikePrefab;
    public GameObject lazerPrefab;
    int numberloops = 8;
    int numberloops2 = 7;
    int loopstoscene2 = 20;
    int loopstoscene3 = 50;
    GameObject actualLazer;
    int spikeDifficulty = 2;
    int barrelNumbers = 6;
    GameObject[] barril = new GameObject[6];
    GameObject[] spike = new GameObject[5];
    GameObject barrilEscudo;
    GameObject barrilDouble;
    float[] randomx = new float[6];
    float[] randomy = new float[6];
    int[] valuesToBeSkipped = new int[5];
    public int resetCounter = 0;
    int umoudois;

    public void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            switch (valuesToBeSkipped[i])
            {
                case (0):
                    valuesToBeSkipped[i] = 5;
                    break;
                case (1):
                    valuesToBeSkipped[i] = 9;
                    break;
                case (2):
                    valuesToBeSkipped[i] = 13;
                    break;
                case (3):
                    valuesToBeSkipped[i] = 17;
                    break;
                case (4):
                    valuesToBeSkipped[i] = 21;
                    break;
                case (5):
                    valuesToBeSkipped[i] = 25;
                    break;
            }
        }
         umoudois = Random.Range(1, 2);
    }

    public void RandomSpawn()
    {
        Destroy(barrilEscudo);
        Destroy(barrilDouble);
        for (int i = 0; i < barrelNumbers; i++)
        {
            Destroy(barril[i]);
        }
        for (int i = 0; i < spikeDifficulty; i++)
        {
            Destroy(spike[i]);
        }
        Destroy(actualLazer);
        resetCounter++;
        if (resetCounter == 5 || resetCounter == 15)
        {
            spikeDifficulty++;
        }

        if (resetCounter == 30)
        {
            spikeDifficulty++;
            barrelNumbers--;
        }
    }

    public void RandomSpawnBarrilEscudo()
    {
        if ( (GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) == numberloops)
        {
            numberloops = numberloops + 2;
            if ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) < loopstoscene2)
            {
                if (umoudois == 1)
                {
                    randomx[1] = Random.Range(-5.9f, 19.7f);
                    randomy[1] = Random.Range(17f, 32.19f);
                }
                else
                {
                    randomx[1] = Random.Range(-5.9f, 19.7f);
                    randomy[1] = Random.Range(47.381f, 62.57f);
                }

                barrilEscudo = Instantiate(barrilEscudoPrefab, new Vector3(randomx[1], randomy[1], 0), Quaternion.Euler(0, 0, 0)) as GameObject;
                umoudois = Random.Range(1, 2);
            }

            else if (((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) >= loopstoscene2) && ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) < loopstoscene3))
            {
                randomx[1] = Random.Range(-5.9f, 19.7f);
                randomy[1] = Random.Range(132.47f, 132.98f);

                barrilEscudo = Instantiate(barrilEscudoPrefab, new Vector3(randomx[1], randomy[1], 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            }

            else if ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) >= loopstoscene3)
            {
                randomx[1] = Random.Range(-5.9f, 19.7f);
                randomy[1] = Random.Range(237.281f, 261.25f);

                barrilEscudo = Instantiate(barrilEscudoPrefab, new Vector3(randomx[1], randomy[1], 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            }
        }
    }

    public void RandomSpawnBarrilDouble()
    {
        if ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) == numberloops2)
        {
            numberloops2 = numberloops2 + 1;
            if ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) < loopstoscene2)
            {
                if (umoudois == 1)
                {
                    randomx[1] = Random.Range(-5.9f, 19.7f);
                    randomy[1] = Random.Range(17f, 32.19f);
                }
                else
                {
                    randomx[1] = Random.Range(-5.9f, 19.7f);
                    randomy[1] = Random.Range(47.381f, 62.57f);
                }

                barrilDouble = Instantiate(barrilDoublePrefab, new Vector3(randomx[1], randomy[1], 0), Quaternion.Euler(0, 0, 0)) as GameObject;
                umoudois = Random.Range(1, 2);
            }

            else if (((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) >= loopstoscene2) && ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) < loopstoscene3))
            {
                randomx[1] = Random.Range(-5.9f, 19.7f);
                randomy[1] = Random.Range(132.47f, 132.98f);

                barrilDouble = Instantiate(barrilDoublePrefab, new Vector3(randomx[1], randomy[1], 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            }

            else if ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) >= loopstoscene3)
            {
                randomx[1] = Random.Range(-5.9f, 19.7f);
                randomy[1] = Random.Range(237.281f, 261.25f);

                barrilDouble = Instantiate(barrilDoublePrefab, new Vector3(randomx[1], randomy[1], 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            }
            
        }


    }

    public void RandomSpawnBack()
    {
        if ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) < loopstoscene2)
        {
            for (int i = 0; i < barrelNumbers; i++)
            {
                if (i == 0)
                {
                    randomx[i] = Random.Range(-5.9f, 19.7f);
                    randomy[i] = Random.Range(17f, 32.19f);
                }
                else if (i == 1)
                {
                    randomx[i] = Random.Range(-5.9f, 19.7f);
                    randomy[i] = Random.Range(17f, 32.19f);
                }

                else if (i == 2)
                {
                    randomx[i] = Random.Range(-5.9f, 19.7f);
                    randomy[i] = Random.Range(17f, 32.19f);
                }
                else if (i == 3)
                {
                    randomx[i] = Random.Range(-5.9f, 19.7f);
                    randomy[i] = Random.Range(47.381f, 62.57f);
                }
                else if (i == 4)
                {
                    randomx[i] = Random.Range(-5.9f, 19.7f);
                    randomy[i] = Random.Range(47.381f, 62.57f);
                }

                else if (i == 5)
                {
                    randomx[i] = Random.Range(-5.9f, 19.7f);
                    randomy[i] = Random.Range(47.381f, 62.57f);
                }

                barril[i] = Instantiate(barrilPrefab, new Vector3(randomx[i], randomy[i], 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            }
        }

        else if ( ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) >= loopstoscene2) && ( (GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) < loopstoscene3))
        {
            barrelNumbers = 3;
            for (int i = 0; i < barrelNumbers; i++)
            {

                if (i == 0)
                {
                    randomx[i] = Random.Range(-5.9f, 19.7f);
                    randomy[i] = Random.Range(132.47f, 132.98f);
                }
                else if (i == 1)
                {
                    randomx[i] = Random.Range(-5.9f, 19.7f);
                    randomy[i] = Random.Range(132.47f, 132.98f);
                }

                else if (i == 2)
                {
                    randomx[i] = Random.Range(-5.9f, 19.7f);
                    randomy[i] = Random.Range(132.47f, 132.98f);
                }

                barril[i] = Instantiate(barrilPrefab, new Vector3(randomx[i], randomy[i], 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            }
        }

        else if ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) >= loopstoscene3)
        {
            barrelNumbers = 2;
            for (int i = 0; i < barrelNumbers; i++)
            {

                if (i == 0)
                {
                    randomx[i] = Random.Range(-5.9f, 19.7f);
                    randomy[i] = Random.Range(237.281f, 261.25f);
                }
                else if (i == 1)
                {
                    randomx[i] = Random.Range(-5.9f, 19.7f);
                    randomy[i] = Random.Range(237.281f, 261.25f);
                }

                barril[i] = Instantiate(barrilPrefab, new Vector3(randomx[i], randomy[i], 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            }
        }

    }

    public void RandomSpawnSpike()
    {
        if ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) < loopstoscene2)
        {
            for (int i = 0; i < spikeDifficulty; i++)
            {
                randomx[i] = Random.Range(-5.9f, 19.7f);
                randomy[i] = Random.Range(32.191f, 47.38f);
                spike[i] = Instantiate(spikePrefab, new Vector3(randomx[i], randomy[i], 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            }
        }

        else if (((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) >= loopstoscene2) && ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) < loopstoscene3))
        {
            for (int i = 0; i < spikeDifficulty; i++)
            {
                randomx[i] = Random.Range(-5.9f, 19.7f);
                randomy[i] = Random.Range(132.981f, 143.49f);
                spike[i] = Instantiate(spikePrefab, new Vector3(randomx[i], randomy[i], 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            }
        }


        else if ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) >= loopstoscene3)
        {
            for (int i = 0; i < spikeDifficulty; i++)
            {
                randomx[i] = Random.Range(-5.9f, 19.7f);
                randomy[i] = Random.Range(239.2656f, 251.25f);
                spike[i] = Instantiate(spikePrefab, new Vector3(randomx[i], randomy[i], 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            }
        }

    }

    
public void LazerSpawn()
    {
        bool contain=false;
        for (int i = 0; i < 5; i++)
        {
            if (resetCounter == valuesToBeSkipped[i])
            {
                contain = true;
            }
        }
        if (resetCounter%2!=0 && !contain)
        {
            if ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) < loopstoscene2)
            {
                actualLazer = Instantiate(lazerPrefab, new Vector3(0.25f, Random.Range(24, 30)), Quaternion.Euler(0, 0, 0)) as GameObject;
            }

            else if (((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) >= loopstoscene2) && ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) < loopstoscene3))
            {
                actualLazer = Instantiate(lazerPrefab, new Vector3(0.25f, Random.Range(129.47f, 143.49f)), Quaternion.Euler(0, 0, 0)) as GameObject;
            }

            else if ((GameObject.Find("RussianGuy").GetComponent<Controller2D>().teleportCount) >= loopstoscene3)
            {
                actualLazer = Instantiate(lazerPrefab, new Vector3(0.25f, Random.Range(237.281f, 251.25f)), Quaternion.Euler(0, 0, 0)) as GameObject;
            }

        }
    }


}