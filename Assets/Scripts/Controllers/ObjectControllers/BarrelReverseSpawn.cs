using UnityEngine;
using System.Collections;

public class BarrelReverseSpawn : MonoBehaviour
{
    public void BarrilSpawno()
    {
        gameObject.GetComponent<Animator>().SetBool("Spawno", true);
    }
}
