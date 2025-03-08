using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{

    public float TimeLeft = 300;
    public bool TimerOn = false;

    public TMP_Text TimerTxt;

    public void Start()
    {
        TimerOn = true;
    }

    public void Update()
    {
        if(TimerOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }

            else
            {
                TimeLeft = 4139;
                TimerOn = false;
                updateTimer(TimeLeft);

                SceneManager.LoadScene(4); // Charge la scène 1
                
            }
        }
    }

    public void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
     
     public void restarter()
     {
        TimerOn = true;
     }
}

