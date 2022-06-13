using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
[System.Serializable]
public class Room_List
{
    public int gameroomId;
    public string title;
    public int wordCount;
    public int playerCount;
    public string gameCharacter;
}




public class RoomListmanager : MonoBehaviour
{
    public GameObject RoomComponent;
    private List<GameObject> RoomComponent_Instance = new List<GameObject>();
    

    public void MakeRoom(Room_List[] RoomList)
    {
        for (int i = 0; i < RoomList.Length; i++)
        {
            RoomComponent_Instance.Add(Instantiate(RoomComponent));
            RoomComponent_Instance[i].transform.SetParent(GameObject.Find("Content").transform, false);
            RoomComponent_Instance[i].GetComponent<RoomComponent>().Index_setter(i);
            RoomComponent_Instance[i].GetComponent<RoomComponent>().gameroomId_setter(RoomList[i].gameroomId.ToString());
            RoomComponent_Instance[i].GetComponent<RoomComponent>().title_setter(RoomList[i].title);
            RoomComponent_Instance[i].GetComponent<RoomComponent>().wordCount_setter(RoomList[i].wordCount.ToString());
            RoomComponent_Instance[i].GetComponent<RoomComponent>().playerCount_setter(RoomList[i].playerCount.ToString());
            RoomComponent_Instance[i].GetComponent<RoomComponent>().gameCharacter_setter(RoomList[i].gameCharacter);
        }
    }
    public GameObject getRoomList(int index) { 
        return RoomComponent_Instance[index];
    }

}
