using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour
{
    public bool firstTouch;
    public Touch touch;
    public float XAxis(Transform positionInit)
    {
        float x = 0;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                x = Camera.main.ScreenToWorldPoint(touch.position).x - positionInit.position.x;
                x = Mathf.Sign(x);
            }
        }
        return x;
    }
    public float YAxis(Transform positionInit)
    {
        float y = 0;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                y = Camera.main.ScreenToWorldPoint(touch.position).y - positionInit.position.y;
                y = Mathf.Sign(y);
            }
        }
        return y;
    }
    public void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            if (firstTouch == false)
            {
                firstTouch = true;
            }
            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);
            }
        }
    }
}
