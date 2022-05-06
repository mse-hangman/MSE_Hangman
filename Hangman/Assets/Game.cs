using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Game : MonoBehaviour
{
    Word word = new Word();
    string curWord;
    public TextMeshProUGUI txt;
    private string reponse;
    private bool win = false;
    public Sprite[] sp;
    public GameObject bear;

    private int death_count=1;

    private void Awake()
    {
        curWord = word.GetWord();
        Debug.Log(curWord);
        
        for (int i = 0; i < word.curWord.Length; i++)
        {
            reponse += "_";
        }
        txt.text = reponse;
        reponse = null;
    }
   public void KeyboardPress(string letter)
    {
        Validation(letter);
        Debug.Log("Validation  "+letter);
    }
    private void Validation(string letter)
    {
        reponse = "";
        win = false;

        for(int i = 0; i < word.curWord.Length; i++)
        {
            
            if (txt.text.Substring(i, 1) == "_")
            {
                if (word.curWord.Substring(i, 1) == letter)
                {
                    reponse += letter;
                    win = true;
                }
                else
                {
                    reponse += "_";
                }
            }
            else
            {
                reponse += txt.text.Substring(i, 1);
            }
        }
        txt.text = reponse;
        Verification();
    }
    void Verification()
    {
        if(win)
        {
            if (txt.text ==word.curWord)
            {

                Debug.Log("win");
            }
        }
        else
        {
            bear.GetComponent<Image>().sprite = sp[death_count];
            death_count++;
            if (death_count == 9)
            {
                Debug.Log("lose");
            }

        }
    }

}
