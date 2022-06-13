using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
   {
    "userId": "ppl3",
    "win": 2,
    "lose": 0,
    "score": 2
  },
  {
    "userId": "ppl4",
    "win": 0,
    "lose": 2,
    "score": -2
  }
 */
public class RankingComponent : MonoBehaviour
{
    public TextMeshProUGUI userId;
    public TextMeshProUGUI win;
    public TextMeshProUGUI lose;
    public int index;
    public void userId_setter(string setter)
    {
        userId.text = setter;
    }
    public void win_setter(string setter)
    {
        win.text = setter;
    }
    public void lose_setter(string setter)
    {
        lose.text = setter;
    }
    public void Index_setter(int setter)
    {
        index= setter;
    }
    public int Index_getter()
    {
        return index;
    }
}
