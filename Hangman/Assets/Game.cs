using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Game : MonoBehaviour
{
    Word word;
    string curWord;
    public TextMeshProUGUI txt;
    
    private string reponse;
    private bool win = false;
    public Sprite[] sp;
    public GameObject bear;
    public GameObject resultPanel;
    public GameObject answerPanel;

    private int death_count=1;

    private void Start()
    {
        word = new Word();
        word.Setting();
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
        Debug.Log("Validation  "+letter+"\n"+ word.curWord.Length);
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
                resultPanel.SetActive(true);
                resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You Win";
                Debug.Log("win");
            }
        }
        else
        {
            death_count++;
            bear.GetComponent<Image>().sprite = sp[death_count];
            if (death_count == 9)
            {
                resultPanel.SetActive(true);
                resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You Lose";
                Debug.Log("lose");
            }

        }
    }
    public void SetActiveAnswer()
    {
        answerPanel.SetActive(true);
    }
    public void Direct_Verification()
    {
        if (curWord == answerPanel.GetComponentInChildren<TMP_InputField>().text)
        {
            answerPanel.SetActive(false);
            resultPanel.SetActive(true);
            resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You Win";
        }
        else
        {
            answerPanel.SetActive(false);
            resultPanel.SetActive(true);
            resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You Lose";
        }
    }
}
