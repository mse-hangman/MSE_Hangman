using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class  Word : MonoBehaviour
{
    private List<string> word= new List<string>();
    public string curWord;
    
     public Word()
    {
        word.Add("HELLOIS");
        word.Add("MYNAMHI");
    }
    public string GetWord()
    {
        curWord = word[Random.Range(0, word.Count)];
        return curWord;
    }
 
}
