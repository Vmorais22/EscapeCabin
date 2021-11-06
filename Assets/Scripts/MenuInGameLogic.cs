using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInGameLogic : MonoBehaviour
{
    public GameObject CanvasMenu;
    private Vector3 aux;
    private Quaternion auxr;
    private bool menuon = false;
    static private float luz = 0.1f;
    static private float velocidad = 3.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("2") || Input.GetButtonDown("Fire2")) && !menuon)//
        {
            menuon = true;
            aux = Camera.main.transform.position;
            auxr = Camera.main.transform.rotation;
            CanvasMenu.transform.SetParent(GameObject.Find("Main Camera").transform);
            CanvasMenu.transform.localPosition = new Vector3(0f, 0f, 0.3f);
            Vector3 pos = CanvasMenu.transform.position;
            Quaternion rot = GameObject.Find("Main Camera").transform.rotation;
            new WaitForSeconds(1f);
            CanvasMenu.transform.parent = null;
            CanvasMenu.transform.position = pos;
            CanvasMenu.transform.rotation = rot;
            CanvasMenu.SetActive(true);
            TimerLogic.stopTimer = true;
            PlayerMove.menuON = true;
        }
        else if ((Input.GetKeyDown("2") || Input.GetButtonDown("Fire2")) && menuon)//
        {
            menuon = false;
            CanvasMenu.SetActive(false);
            TimerLogic.stopTimer = false;
            PlayerMove.menuON = false;
        }
    }

    static public void ChangeLight(bool subir)
    {
        if(subir && luz<1.5f)
        {
            luz += 0.5f;
            GameObject.Find("Directional Light").GetComponent<Light>().intensity = luz;//0.1, 0.6, 1.1, 1.6
            GameObject.Find("SliderLuz").GetComponent<Slider>().value += 1f;
        }
        else if (!subir && luz>0.2f)
        {
            luz -= 0.5f;
            GameObject.Find("Directional Light").GetComponent<Light>().intensity = luz;//0.1, 0.6, 1.1, 1.6
            GameObject.Find("SliderLuz").GetComponent<Slider>().value -= 1f;
        }
    }

    static public void ChangeSpeed(bool subir)
    {
        if (subir)GameObject.Find("SliderVelocidad").GetComponent<Slider>().value += 1f;
        else GameObject.Find("SliderVelocidad").GetComponent<Slider>().value -= 1f;
    }

    static public void Daltonismo(bool dal)
    {
        if (dal) GameObject.Find("Daltonismo").GetComponent<Toggle>().isOn = dal;
        else GameObject.Find("Daltonismo").GetComponent<Toggle>().isOn = dal;
    }

    static public void ColorI(int val)//1 rojo -423, 2 verde 266, 3 azul 950, 4 amarillo 1620, 5 rosa 2225
    {
        if (val == 1)GameObject.Find("FlechaIR").transform.localPosition = new Vector3(-423f, -476f, 0f);
        else if (val == 2)GameObject.Find("FlechaIR").transform.localPosition = new Vector3(266f, -476f, 0f);
        else if (val == 3)GameObject.Find("FlechaIR").transform.localPosition = new Vector3(950f, -476f, 0f);
        else if (val == 4)GameObject.Find("FlechaIR").transform.localPosition = new Vector3(1620f, -476f, 0f);
        else GameObject.Find("FlechaIR").transform.localPosition = new Vector3(2225f, -476f, 0f);
    }

    static public void ColorC(int val)//1 rojo -423, 2 verde 266, 3 azul 950, 4 amarillo 1620, 5 rosa 2225
    {
        if (val == 1) GameObject.Find("FlechaC").transform.localPosition = new Vector3(-423f, -1327f, 0f);
        else if (val == 2) GameObject.Find("FlechaC").transform.localPosition = new Vector3(266f, -1327f, 0f);
        else if (val == 3) GameObject.Find("FlechaC").transform.localPosition = new Vector3(950f, -1327f, 0f);
        else if (val == 4) GameObject.Find("FlechaC").transform.localPosition = new Vector3(1620f, -1327f, 0f);
        else GameObject.Find("FlechaC").transform.localPosition = new Vector3(2225f, -1327f, 0f);
    }

}
