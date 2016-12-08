using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RussianUI : MonoBehaviour {

    Text directionText;
    void start()
    {
    }
    public void ChangeDirection(int direction)
    {
        directionText = GetComponent<Text>();
        switch (direction)
        {
            case 1:
                {
                    directionText.text = ("Up");
                    break;
                }
            case 2:
                {
                    directionText.text = ("Right");
                    break;
                }
            case 3:
                {
                    directionText.text = ("Down");
                    break;
                }
            case 4:
                {
                    directionText.text = ("Left");
                    break;
                }
        }
    }
}
