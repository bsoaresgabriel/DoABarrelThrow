using UnityEngine;
using System.Collections;

public class ObjectChanger : MonoBehaviour
{
    public IEnumerator TransformaLayer(GameObject obj)
    {
        obj.layer = 10;
       yield return new WaitForSeconds(1f);
       obj.layer = 8;
    }
}
