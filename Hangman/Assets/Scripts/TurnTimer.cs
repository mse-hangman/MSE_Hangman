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
            ///ChangeStopTimer(true,"other");//Turn 넘겨주는거
            //GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().TurnStart("other");
            Destroy(gameObject);
        }
        if(stopTimer == false)
        {
            timerText.text = textTime;
            timerSlider.value = time;
        }
    }
    public void Turnchange (string start_player)
    {
        if (start_player == "other")
        {
            turnText.text = "Other Player Turn";
        }
        else if (start_player == "my")
        {
            turnText.text = "My Turn";
        }
    }
    public bool GetStopTimer()
    {
        return stopTimer;
    }
}
