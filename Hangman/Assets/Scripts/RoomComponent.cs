using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomComponent : MonoBehaviour
{
    public TextMeshProUGUI gameroomId;
    public TextMeshProUGUI title;
    public TextMeshProUGUI wordCount;
    public TextMeshProUGUI playerCount;
    public TextMeshProUGUI gameCharacter;
    public int index;
    public void gameroomId_setter(string setter)
    {
        gameroomId.text = setter;
    }
    public void title_setter(string setter)
    {
        title.text = setter;
    }
    public void wordCount_setter(string setter)
    {
        wordCount.text = setter;
    }
    public void playerCount_setter(string setter)
    {
        playerCount.text = setter;
    }
     public void gameCharacter_setter(string setter)
     {
        gameCharacter.text = setter;
     }
    public void Index_setter(int setter)
    {
        index= setter;
    }
    public int Index_getter()
    {
        return index;
    }
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            GameObject.Find("GameManger").GetComponent<gamemanager>().enter_room(gameObject.GetComponent<RoomComponent>().Index_getter());
        });
    }
    
}
