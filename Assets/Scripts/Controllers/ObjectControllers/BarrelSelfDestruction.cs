using UnityEngine;
using System.Collections;

public class BarrelSelfDestruction : MonoBehaviour
{
    void Update()
    {
        if (FindObjectOfType<Controller2D>().collisions.justReset)
        {
            Destroy(gameObject);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
