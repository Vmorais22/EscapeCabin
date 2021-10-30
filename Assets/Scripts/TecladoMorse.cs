using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TecladoMorse : MonoBehaviour
{

    public bool tecla1 = false;
    public bool tecla2 = false;
    public bool tecla3 = false;
    public bool tecla4 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Primera()
    {
        tecla1 = true;
        //sonido bueno
    }

    public void Segunda()
    {
        tecla2 = true;
        //sonido bueno
    }

    public void Tercera()
    {
        tecla3 = true;
        //sonido bueno
    }

    public void Quarta()
    {
        tecla4 = true;
        //Se abre la puerta correspondiente y sonido
    }

    public void Reseteo()
    {
        tecla1 = false;
        tecla2 = false;
        tecla3 = false;
        tecla4 = false;
        //sonido malo
    }
}
