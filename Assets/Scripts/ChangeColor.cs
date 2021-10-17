using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour

{
    // Start is called before the first frame update
    public Material cogido;
    public Material arrastrable;
    void Start()
    {
        Material iniMaterial = GetComponent<Renderer>().material;
    }

    void changeColor( Renderer obj, int tag)
    {
        if(tag == 1) //cogido
        {
            obj.material = cogido;
        }
        else if (tag == 2) //cogido
        {
            obj.material = arrastrable;
        }
    }

}
