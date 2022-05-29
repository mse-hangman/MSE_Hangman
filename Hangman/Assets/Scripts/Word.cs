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
    public List<string> word= new List<string>();
    public string curWord;
    public void Setting(string words)
    {
        ThreeWord threeword=new ThreeWord();
        threeword = JsonUtility.FromJson<ThreeWord>(words);
        word = threeword.result;
        Debug.Log(word[0]);
    }
    public string GetWord(int index)
    {
        curWord = word[index].ToUpper();
        Debug.Log(curWord);
        return curWord;
    }

    public List<string> getThreeWord()
    {
        return word; 
    }
}
