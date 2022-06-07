using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class WaitingRoom_Manager : MonoBehaviour
{
    public TextMeshProUGUI Ready;
    public TextMeshProUGUI OtherPlayerReady;
    public Image Player1Image;
    public Image Player2Image;
    public Sprite []images;

    public Button StartButton;

    private void Awake()
    {
        Player1Image = GameObject.Find("Player1 Image").GetComponent<Image>();
        Player2Image = GameObject.Find("Player2 Image").GetComponent<Image>();
        Debug.Log(Player1Image);
    }
    public void setcharacter(string character)
    {
        if (character=="cat1") {
            Player1Image.sprite = images[0];
            Player2Image.sprite = images[0];
        }
        else if (character == "cat2")
        {
            Player1Image.sprite = images[1];
            Player2Image.sprite = images[1];
        }
        else if (character == "lion")
        {
            Player1Image.sprite = images[2];
            Player2Image.sprite = images[2];
        }
        else if (character == "seaotter")
        {
            Player1Image.sprite = images[3];
            Player2Image.sprite = images[3];
        }
    }
}
