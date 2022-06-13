using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseBtn : MonoBehaviour
{
   public void close_Btn()
    {
        Application.Quit();
    }
    public void back_lobby_Btn()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
