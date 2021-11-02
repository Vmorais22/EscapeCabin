using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLogical : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject creditPanel;
    public GameObject aboutPanel;
    public GameObject exitPanel;
    public GameObject instrucciones;
    public GameObject cargando;
    public float sightlength = 10f;
    public Text textoDificultat;
    private RaycastHit _hit;
    public static int dif = 1;


    // Use this for initialization
    void Start()
    {
        mainMenuPanel.SetActive(true);
        creditPanel.SetActive(false);
        aboutPanel.SetActive(false);
        exitPanel.SetActive(false);
        instrucciones.SetActive(true);
        cargando.SetActive(false);
    }

    private void Update()
    {
        GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.white;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out _hit, sightlength))
        {
            Debug.Log("Entrando en Physics");
            if (_hit.transform.CompareTag("ButtonPlay")){
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
                Debug.Log("BotonPlay");
                if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
                {
                    StartGameClick();
                }
            }
            if (_hit.transform.CompareTag("ButtonAbout"))
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
                Debug.Log("BotonAbout");
                if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
                {
                    AboutClicked();
                }
            }
            if (_hit.transform.CompareTag("ButtonCredits"))
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
                Debug.Log("BotonCredits");
                if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
                {
                    CreditClicked();
                }
            }
            if (_hit.transform.CompareTag("ButtonExit"))
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
                Debug.Log("BotonExit");
                if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
                {
                    ExitClicked();
                }
            }
            if (_hit.transform.CompareTag("ButtonBack"))
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
                Debug.Log("BotonBack");
                if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
                {
                    BackClicked();
                }
            }
            if (_hit.transform.CompareTag("ButtonYes"))
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
                Debug.Log("BotonYes");
                if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
                {
                    YesGameClick();
                }
            }
            if (_hit.transform.CompareTag("ButtonFacil"))
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
                Debug.Log("BotonYes");
                if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
                {
                    CambiaDificultat("Facil");
                }
            }
            if (_hit.transform.CompareTag("ButtonIntermedio"))
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
                Debug.Log("BotonYes");
                if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
                {
                    CambiaDificultat("Intermedia");
                }
            }
            if (_hit.transform.CompareTag("ButtonDificil"))
            {
                GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().MaterialComp.color = Color.red;
                Debug.Log("BotonYes");
                if (Input.GetButtonDown("TriggerAbajo") || Input.GetButtonDown("Fire1"))
                {
                    CambiaDificultat("Dificil");
                }
            }
        }
    }

    public void StartGameClick()
    {
        creditPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        aboutPanel.SetActive(false);
        exitPanel.SetActive(false);
        instrucciones.SetActive(false);
        cargando.SetActive(true);
        SceneManager.LoadScene("EscapeRoom");
    }

    //public void LoadScene(string scenename){
    //SceneManager.LoadScene(scenename);
    //}

    public void CreditClicked()
    {
        creditPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        aboutPanel.SetActive(false);
        exitPanel.SetActive(false);
        instrucciones.SetActive(false);
    }

    public void AboutClicked()
    {
        aboutPanel.SetActive(true);
        creditPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        exitPanel.SetActive(false);
        instrucciones.SetActive(false);
    }

    public void ExitClicked()
    {
        exitPanel.SetActive(true);
        aboutPanel.SetActive(false);
        creditPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        instrucciones.SetActive(false);
    }

    public void NoClicked()
    {
        mainMenuPanel.SetActive(true);
        creditPanel.SetActive(false);
        aboutPanel.SetActive(false);
        exitPanel.SetActive(false);
        instrucciones.SetActive(true);
    }

    public void YesGameClick()
    {
        Application.Quit();
    }


    public void BackClicked()
    {
        mainMenuPanel.SetActive(true);
        creditPanel.SetActive(false);
        aboutPanel.SetActive(false);
        exitPanel.SetActive(false);
        instrucciones.SetActive(true);
    }

    public void CambiaDificultat(string dificultat)
    {
        textoDificultat.GetComponent<Text>().text = "La dificultad seleccionada es: " + dificultat;
        if(dificultat == "Facil") dif = 1;
        else if(dificultat == "Intermedia") dif = 2;
        else dif = 3;
        mainMenuPanel.SetActive(true);
        creditPanel.SetActive(false);
        aboutPanel.SetActive(false);
        exitPanel.SetActive(false);
        instrucciones.SetActive(true);
        cargando.SetActive(false);
    }

}
