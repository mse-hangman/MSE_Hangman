using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToOtherScene : MonoBehaviour
{
    public GameObject Network_manager;

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
        Network_manager = GameObject.Find("network_manager");
        Network_manager.gameObject.GetComponent<Network_manager>().GetThreeWord();
        SceneManager.LoadScene("GameScene");
    }
}
