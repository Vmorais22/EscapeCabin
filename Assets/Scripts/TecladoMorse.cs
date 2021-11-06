using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TecladoMorse : MonoBehaviour
{

    public bool tecla1 = false;
    public bool tecla2 = false;
    public bool tecla3 = false;
    public bool tecla4 = false;
    public AudioSource correcto;
    public AudioSource incorrecto;


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
        correcto.Play();
    }

    public void Segunda()
    {
        tecla2 = true;
        correcto.Play();
    }

    public void Tercera()
    {
        tecla3 = true;
        correcto.Play();
    }

    public void Quarta()
    {
        tecla4 = true;
        correcto.Play();
    }

    public void Reseteo()
    {
        tecla1 = false;
        tecla2 = false;
        tecla3 = false;
        tecla4 = false;
        incorrecto.Play();
    }
}
