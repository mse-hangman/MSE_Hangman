using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeWord//
{
    public int statusCod;
    public List<string> result=new List<string>();
    
}

public class  Word : MonoBehaviour
{
    public GameObject Network_manager;
    private List<string> word= new List<string>();
    public string curWord;
    public void Setting()
    {
        ThreeWord threeword=new ThreeWord();
        Network_manager=GameObject.Find("network_manager");
        string words= Network_manager.GetComponentInChildren<Network_manager>().GetDownloaddata();//
        threeword = JsonUtility.FromJson<ThreeWord>(words);
        word = threeword.result;
    }


    public string GetWord()
    {
        curWord = word[Random.Range(0, word.Count)].ToUpper();
        Debug.Log(curWord);
        return curWord;
    }
 
}
