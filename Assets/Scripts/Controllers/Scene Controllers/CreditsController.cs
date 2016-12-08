using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour {

    private bool EstaNaCorotina;
    private float eixoy;

    IEnumerator CountdownRotina()
    {
        EstaNaCorotina = true;
        yield return new WaitForSeconds(0.011f);
        EstaNaCorotina = false;

        yield break;
    }

    void ChangePosition()
    {
        if (eixoy < 37f)
        {
            transform.position = new Vector3(0, eixoy + 0.1f, 0);
            eixoy = eixoy + 0.1f;
        }
        else
        {
            Debug.Log("voltou o bang");
            transform.position = new Vector3(0, -35f, 0);
            eixoy = -35f;
        }

    }

    // Use this for initialization
    void OnEnable () {
        EstaNaCorotina = false;
        eixoy = -35f;
    }
	
	// Update is called once per frame
	void Update () {
        if ((GameObject.Find("Canvas").GetComponent <MenuPrincipal> ().CreditosON) == true )
        {
            if (EstaNaCorotina == false)
            {
                ChangePosition();
                StartCoroutine(CountdownRotina());
            }

        }

    }
}
