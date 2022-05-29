using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TurnTimer : MonoBehaviour
{
    public Slider timerSlider;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI turnText;
    public float gameTime;

    private bool stopTimer;
    void Start()
    {
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        float time = gameTime - Time.time;

        int seconds = Mathf.FloorToInt(time);

        string textTime = seconds.ToString();

        if(time <= 0)
        {
            stopTimer = true;
            if(turnText.text=="My Turn")
            {
                turnText.text = "Other Player Turn";
            }
            else if (turnText.text == "Other Player Turn")
            {
                turnText.text = "My Turn";
            }
        }
        if(stopTimer == false)
        {
            timerText.text = textTime;
            timerSlider.value = time;
        }
    }
}
