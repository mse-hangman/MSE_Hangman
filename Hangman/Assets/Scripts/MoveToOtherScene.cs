using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToOtherScene : MonoBehaviour
{
    public void MoveToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public void MoveToWaiting()
    {
        SceneManager.LoadScene("WaitingRoomScene");
    }
    public void MoveToGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
