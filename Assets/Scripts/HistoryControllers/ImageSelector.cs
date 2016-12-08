using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageSelector : MonoBehaviour
{
    public Image[] imageHistoryCollection;
    public Image currentImage;
    void Awake()
    {
        print("meCHamou");
        currentImage.sprite = imageHistoryCollection[Random.Range(0, imageHistoryCollection.Length - 1)].sprite;
    }
}
