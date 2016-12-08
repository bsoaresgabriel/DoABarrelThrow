using UnityEngine;
using System.Collections;

public class CharacterChanger : MonoBehaviour {
    public GameObject[] characters = new GameObject[2];
	// Use this for initialization
	void Start ()
    {
	}
	public void trocaChars(GameObject currChar)
    {
        Vector3 charPos;
        Quaternion charRot;
        charPos = currChar.transform.position;
        charRot = currChar.transform.rotation;
        for (int i = 0; i < 2; i++)
        {
            if (characters[i].tag != currChar.tag)
            {
                Destroy(currChar);
                Instantiate(characters[i], charPos, charRot);
                Camera.main.GetComponent<CameraFollow>().target = FindObjectOfType<Controller2D>();
            }
        }
    }
}
