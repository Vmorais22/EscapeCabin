using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerLogic : MonoBehaviour
{
    public GameObject textDisplay;
    public int  minutes = 0;
    private int seconds = 0;
    public bool takingAway = false;
    public static bool stopTimer = false;

    private void Start()
    {
        int aux = MenuLogical.dif;
        if (aux == 1) minutes = 8;
        else if (aux == 2) minutes = 5;
        else minutes = 3;
        textDisplay.GetComponent<Text>().text = "0" + minutes + ":00";
    }

    private void Update()
    {
        if(takingAway == false && stopTimer == false) StartCoroutine(TimerTake());
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        if (seconds == 0 && minutes == 0) textDisplay.GetComponent<Text>().text = "GameOver";
        else if (seconds == 0 && minutes > 0)
        {
            textDisplay.GetComponent<Text>().text = "0" + minutes + ":00";
            --minutes;
            seconds = 59;
        }
        else if (seconds < 10)
        {
            textDisplay.GetComponent<Text>().text = "0" + minutes + ":0" + seconds;
            --seconds;
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "0" + minutes + ":" + seconds;
            --seconds;
        }
        takingAway = false;
    }

    public void SetMinutes(int min)
    {
        minutes = min;
    }
}
