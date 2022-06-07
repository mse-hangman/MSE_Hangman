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
    public GameObject NotMyTurn;
    public GameObject NotMyTurn_instance;

    private string reponse;
    private bool win = false;
    public Sprite[] now_sprites;
    public Sprite[] lion;
    public Sprite[] cat1;
    public Sprite[] cat2;
    public Sprite[] seaotter;

    public Sprite[] dice;

    public GameObject Timer;
    public GameObject Timer_Instance;
    public GameObject GaurdPanel;
    public GameObject GaurdPanel_Instance;

    public GameObject MyDiceani;
    public GameObject OtherDiceani;

    public GameObject Mycharacter;
    public GameObject Enemycharacter;

    public GameObject resultPanel;
    public GameObject answerPanel;

    private int death_count=1;
    private int otherplayer_death_count = 1;
    private void Awake()
    {
        MyDiceani = GameObject.Find("MyDice");
        Mycharacter = GameObject.Find("MyCharacter");//instance·Î ¹Ù²ã¾ß ÇÔ
        Enemycharacter = GameObject.Find("EnemyCharacter");//instance·Î ¹Ù²ã¾ß ÇÔ
        resultPanel = GameObject.Find("GameresultPanel");
        resultPanel.gameObject.SetActive(false);
        answerPanel = GameObject.Find("AnswerPanel");
        answerPanel.gameObject.SetActive(false);
        word = GameObject.Find("HangmanWord").GetComponent<Word>();
        txt = word.GetComponent<TextMeshProUGUI>();
        TurnStart("other");//testcode
    }
    public void SetCharacter(string character)
    {
        if (character == "cat1")
        {
            Enemycharacter.GetComponent<Image>().sprite = cat1[0];
            Mycharacter.GetComponent<Image>().sprite = cat1[0];
            now_sprites = cat1;
        }
        else if (character == "cat2")
        {
            Enemycharacter.GetComponent<Image>().sprite = cat2[0];
            Mycharacter.GetComponent<Image>().sprite = cat2[0];
            now_sprites = cat2;
        }
        else if (character == "lion")
        {
            Enemycharacter.GetComponent<Image>().sprite = lion[0];
            Mycharacter.GetComponent<Image>().sprite = lion[0];
            now_sprites = lion;
        }
        else if (character == "seaotter")
        {
            Enemycharacter.GetComponent<Image>().sprite = seaotter[0];
            Mycharacter.GetComponent<Image>().sprite = seaotter[0];
            now_sprites = seaotter;
        }
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
        txt.text = reponse;
    }
    public int DiceStart()
    {
        MyDiceani = GameObject.Find("MyDice");
        int Result = Random.Range(0, 5);
        StartCoroutine(RollTheDice(MyDiceani.GetComponent<Image>(), Result));
        return Result+1;
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
    public void TurnStart(string turn)
    {
        Debug.Log(turn);
        if (turn == "other")
        {
            GaurdPanel_Instance = Instantiate(GaurdPanel);
            GaurdPanel_Instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
            NotMyTurn_instance = Instantiate(NotMyTurn);
            NotMyTurn_instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }
        if(turn == "my")
        {
            Destroy(GaurdPanel_Instance);
        }

    }
    public void OtherDeathcount()
    {
        otherplayer_death_count--;
    }
    public void KeyboardPress(string letter)
    {
        GameObject.Find("network_manager(Clone)").GetComponent<Network_manager>().SendPlayMessage(Validation(letter));
        Debug.Log("Validation  "+letter+"\n"+ word.curWord.Length);
    }
    private bool Validation(string letter)
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
                    return true;
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
        return false;
    }
    void Verification()
    {
        if(win)
        {
            if (txt.text ==word.curWord)
            {
                GameResult(true);
            }
        }
        else
        {
            death_count++;
            Mycharacter.GetComponent<Image>().sprite = now_sprites[death_count];
            if (death_count == 9)
            {
                GameResult(false);
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
            GameResult(true);
        }
        else
        {
            GameResult(false);
        }
    }
    public void GameResult(bool check)
    {
        if (check == true) {
            answerPanel.SetActive(false);
            resultPanel.SetActive(true);
            resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You Win";
        }
        else if(check == false)
        {
            answerPanel.SetActive(false);
            resultPanel.SetActive(true);
            resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You Lose";
        }
    }
}
