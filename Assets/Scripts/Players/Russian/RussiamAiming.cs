using UnityEngine;
using System.Collections;

public class RussiamAiming : MonoBehaviour
{
    bool pressed,originFound;
    public bool thrown;
    GameObject Arm;
    Vector3 dragOrigin;
    Vector3 difference,origin;
    Vector3 dragFin;
    float distanciaMax = 5;
    float distanciaMaxPow;

void Start()
    {
        distanciaMaxPow = distanciaMax * distanciaMax;
    }
    void Update()
    {
        if (pressed)
        {
            if (!originFound)
            {
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                originFound = true;
            }
            else if (originFound)
            {
                difference = Camera.main.ScreenToWorldPoint(Input.mousePosition)- origin;
                difference.Normalize();
                float rotZ = Mathf.Atan2(difference.y,difference.x)*Mathf.Rad2Deg;
                print(rotZ);
                if (gameObject.tag == "Invert_Aim")
                {
                    rotZ = Mathf.Clamp(rotZ, -60, 60);
                    transform.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                }
                else
                {
                    
                    transform.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                    rotZ = transform.GetChild(3).transform.rotation.eulerAngles.z;
                    rotZ = Mathf.Clamp(rotZ,120, 240);
                    transform.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, rotZ+180);
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && !pressed)
        {
            dragOrigin = Input.mousePosition;
            pressed = true;
        }
        else if (Input.GetMouseButtonUp(0) && pressed)
        {
            dragFin = Input.mousePosition;
            if (dragFin.magnitude > distanciaMaxPow)
            {
                dragFin = dragFin.normalized * distanciaMax;
            }
            pressed = false;
            originFound = false;
            thrown = true;
        }
    }
    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
