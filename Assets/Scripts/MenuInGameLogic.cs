using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInGameLogic : MonoBehaviour
{
    public GameObject CanvasMenu;
    private Vector3 aux;
    private Quaternion auxr;
    private bool menuon = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("2") && !menuon)//|| Input.GetButtonDown("Fire1"))
        {
            menuon = true;
            aux = Camera.main.transform.position;
            auxr = Camera.main.transform.rotation;
            CanvasMenu.transform.SetParent(GameObject.Find("Main Camera").transform);
            CanvasMenu.transform.localPosition = new Vector3(0f, 0f, 0.4f);
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
        else if (Input.GetKeyDown("2") && menuon)//|| Input.GetButtonDown("Fire1"))
        {
            menuon = false;
            CanvasMenu.SetActive(false);
            TimerLogic.stopTimer = false;
            PlayerMove.menuON = false;
        }
    }

    public void ChangeLight(float value)
    {
        if(value == 0f)
        {
            GameObject.Find("Directional Light").GetComponent<Light>().intensity = 0.1f;
        }
        else if (value == 1f)
        {
            GameObject.Find("Directional Light").GetComponent<Light>().intensity = 0.5f;
        }
        else if (value == 2f)
        {
            GameObject.Find("Directional Light").GetComponent<Light>().intensity = 1f;
        }
        else if (value == 3f)
        {
            GameObject.Find("Directional Light").GetComponent<Light>().intensity = 1.5f;
        }
    }
}
