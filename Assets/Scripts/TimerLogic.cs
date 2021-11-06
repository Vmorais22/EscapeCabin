using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerLogic : MonoBehaviour
{
    public GameObject textDisplay;
    public int  minutes = 0;
    private int seconds = 0;
    public bool takingAway = false;
    public static bool stopTimer = false;
    public AudioSource go;

    private void Start()
    {
        int aux = MenuLogical.dif;
        if (aux == 1) minutes = 15;
        else if (aux == 2) minutes = 9;
        else minutes = 5;
        if (minutes < 10) textDisplay.GetComponent<Text>().text = "0" + minutes + ":00";
        else textDisplay.GetComponent<Text>().text = minutes + ":00";
    }

    private void Update()
    {
        if(takingAway == false && stopTimer == false) StartCoroutine(TimerTake());
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        if (seconds == 0 && minutes == 0)
        {
            textDisplay.GetComponent<Text>().text = "GameOver";
            PlayerMove.menuON = true;
            go.Play();
            yield return new WaitForSeconds(4);
            PlayerMove.menuON = false;
            SceneManager.LoadScene("Menu");
        }
        else if (seconds == 0 && minutes > 0)
        {
            if (minutes < 10) textDisplay.GetComponent<Text>().text = "0" + minutes + ":00";
            else textDisplay.GetComponent<Text>().text = minutes + ":00";
            --minutes;
            seconds = 59;
        }
        else if (seconds < 10)
        {
            if (minutes < 10) textDisplay.GetComponent<Text>().text = "0" + minutes + ":0" + seconds;
            else textDisplay.GetComponent<Text>().text = minutes + ":0" + seconds;
            --seconds;
        }
        else
        {
            if (minutes < 10) textDisplay.GetComponent<Text>().text = "0" + minutes + ":" + seconds;
            else textDisplay.GetComponent<Text>().text = minutes + ":" + seconds;
            --seconds;
        }
        takingAway = false;
    }

    public void SetMinutes(int min)
    {
        minutes = min;
    }
}
