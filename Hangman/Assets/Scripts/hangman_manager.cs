using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class hangman_manager : MonoBehaviour
{
    public Word word;
    string curWord;
    public TextMeshProUGUI txt;
    
    private string reponse;
    private bool win = false;
    public Sprite[] sp;
    public Sprite[] dice;

    public GameObject MyDiceani;
    public GameObject OtherDiceani;

    public GameObject character;
    public GameObject resultPanel;
    public GameObject answerPanel;

    private int death_count=1;
    private void Awake()
    {
        MyDiceani = GameObject.Find("MyDice");
        //character = Instantiate();
        resultPanel = GameObject.Find("GameresultPanel");
        answerPanel = GameObject.Find("AnswerPanel");
        word = GameObject.Find("HangmanWord").GetComponent<Word>();
        txt = word.GetComponent<TextMeshProUGUI>();
        Debug.Log(txt.text);
    }
    public void GetWord(string ThreeWord)
    {
        word.Setting(ThreeWord);
        txt.text = reponse;
        reponse = null;
    }
    public void SelectWord(int index)
    {
        curWord = word.GetWord(index);
        Debug.Log(curWord);
        for (int i = 0; i < word.curWord.Length; i++)
        {
            reponse += "_";
        }
    }
    public void DiceStart()
    {
        MyDiceani = GameObject.Find("MyDice");
        int Result = Random.Range(0, 5);
        StartCoroutine(RollTheDice(MyDiceani.GetComponent<Image>(), Result));
    }
    public IEnumerator RollTheDice(Image rend, int result)
    {
        int randomDiceSide = 0;
        int finalSide = 0;
        for (int i = 0; i <= 10; i++)
        {
            randomDiceSide = Random.Range(0, 5);
            rend.sprite = dice[randomDiceSide];
            yield return new WaitForSeconds(0.1f);
        }
        rend.sprite = dice[result];
        finalSide = result+1;
        Debug.Log(finalSide);
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
            character.GetComponent<Image>().sprite = sp[death_count];
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
