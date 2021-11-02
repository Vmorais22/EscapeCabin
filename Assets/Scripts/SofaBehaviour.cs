using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SofaBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public Material[] myMaterials = new Material[7];
    private int actualMaterial = 0;
    private int size = 7;

    void Start()
    {
    }

    // Update is called once per frame
    public void changeSofaColor()
    {
        Debug.Log("actualMaterial: " + actualMaterial + " de: " + size);

        if (actualMaterial >= size)
        {
            Debug.Log("muy grande");
            actualMaterial = 1;
        }
        else
        {
            Debug.Log("Sumamos 1");
            ++actualMaterial;
        }
        GetComponent<Renderer>().material = myMaterials[actualMaterial-1];
        Debug.Log("proximoMaterial: " + actualMaterial + " de: " + size);

    }
}
