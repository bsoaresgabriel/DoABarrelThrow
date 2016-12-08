using UnityEngine;
using System.Collections;

public class MenuCeuAnimation : MonoBehaviour {
    private float eixoy;
    private float eixoyinicial;
    bool EstaNaCorotina;

    IEnumerator CountdownRotina()
    {
        EstaNaCorotina = true;
        yield return new WaitForSeconds(0.1f);
        EstaNaCorotina = false;

        yield break;
    }


    void ChangePosition()
    {
        if (eixoy > -3.58)
        {
            transform.position = new Vector3(0.2f, eixoy - 0.1f, 0);
            eixoy = eixoy - 0.1f;

        }
        else
        {
            eixoy = eixoyinicial;
            transform.position = new Vector3(0.2f, eixoy, 0);
        }

    }
    // Use this for initialization
    void Start()
    {
        eixoy = transform.position.y;
        eixoyinicial = eixoy;
        EstaNaCorotina = false;
        StartCoroutine(CountdownRotina());
    }

    // Update is called once per frame
    void Update()
    {

        if (EstaNaCorotina == false)
        {
            ChangePosition();
            StartCoroutine(CountdownRotina());
        }

    }

}
