using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[System.Serializable]
public class Ranking_List
{
    public string userId;
    public int win;
    public int lose;
    public int score;
}

public class RankingListmanager : MonoBehaviour
{
    public GameObject RankingComponent;
    private List<GameObject> RankingComponent_Instance = new List<GameObject>();
    

    public void MakeRanking(Ranking_List[] RankingList)
    {
        for (int i = 0; i < RankingList.Length; i++)
        {
            RankingComponent_Instance.Add(Instantiate(RankingComponent));
            RankingComponent_Instance[i].transform.SetParent(GameObject.Find("Ranking_Content").transform, false);
            RankingComponent_Instance[i].GetComponent<RankingComponent>().Index_setter(i);
            RankingComponent_Instance[i].GetComponent<RankingComponent>().userId_setter(RankingList[i].userId);
            RankingComponent_Instance[i].GetComponent<RankingComponent>().win_setter(RankingList[i].win.ToString());
            RankingComponent_Instance[i].GetComponent<RankingComponent>().lose_setter(RankingList[i].lose.ToString());
        }
    }
    public GameObject getRankingList(int index) { 
        return RankingComponent_Instance[index];
    }

}
