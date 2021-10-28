using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MenuLogical : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject creditPanel;
    public GameObject aboutPanel;
    public GameObject exitPanel;
    public float sightlength = 10f;
    private RaycastHit _hit;


    // Use this for initialization
    void Start()
    {
        mainMenuPanel.SetActive(true);
        creditPanel.SetActive(false);
        aboutPanel.SetActive(false);
        exitPanel.SetActive(false);

    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out _hit, sightlength))
        {
            Debug.Log("Entrando en Physics");
            if (_hit.transform.CompareTag("ButtonPlay")){
                Debug.Log("BotonPlay");
                if (Input.GetButtonDown("TriggerAbajo"))
                {
                    StartGameClick();
                }
            }
            if (_hit.transform.CompareTag("ButtonAbout"))
            {
                Debug.Log("BotonAbout");
                if (Input.GetButtonDown("TriggerAbajo"))
                {
                    AboutClicked();
                }
            }
            if (_hit.transform.CompareTag("ButtonCredits"))
            {
                Debug.Log("BotonCredits");
                if (Input.GetButtonDown("TriggerAbajo"))
                {
                    CreditClicked();
                }
            }
            if (_hit.transform.CompareTag("ButtonExit"))
            {
                Debug.Log("BotonExit");
                if (Input.GetButtonDown("TriggerAbajo"))
                {
                    ExitClicked();
                }
            }
            if (_hit.transform.CompareTag("ButtonBack"))
            {
                Debug.Log("BotonBack");
                if (Input.GetButtonDown("TriggerAbajo"))
                {
                    BackClicked();
                }
            }
            if (_hit.transform.CompareTag("ButtonYes"))
            {
                Debug.Log("BotonYes");
                if (Input.GetButtonDown("TriggerAbajo"))
                {
                    YesGameClick();
                }
            }
        }
    }

    public void StartGameClick()
    {
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
    }

    public void AboutClicked()
    {
        aboutPanel.SetActive(true);
        creditPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        exitPanel.SetActive(false);
    }

    public void ExitClicked()
    {
        exitPanel.SetActive(true);
        aboutPanel.SetActive(false);
        creditPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
    }

    public void NoClicked()
    {
        mainMenuPanel.SetActive(true);
        creditPanel.SetActive(false);
        aboutPanel.SetActive(false);
        exitPanel.SetActive(false);
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

    }
}
