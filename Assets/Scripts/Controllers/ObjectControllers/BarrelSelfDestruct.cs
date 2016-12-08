using UnityEngine;
using System.Collections;

public class BarrelSelfDestruct : MonoBehaviour
{
    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
