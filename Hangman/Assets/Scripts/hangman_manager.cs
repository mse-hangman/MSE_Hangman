using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class hangman_manager : MonoBehaviour
{
    public Word word;
    string curWord;
    List<char> hintWord = new List<char>();
    public TextMeshProUGUI txt;
    public GameObject NotMyTurn;
    public GameObject NotMyTurn_instance;

    private string reponse;
    private string mean;
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
    public GameObject SelectWordPanel;

    private int death_count=1;
    private int otherplayer_death_count = 1;
    private void Awake()
    {
        Mycharacter = GameObject.Find("MyCharacter");//instance·Î ¹Ù²ã¾ß ÇÔ
        Enemycharacter = GameObject.Find("EnemyCharacter");//instance·Î ¹Ù²ã¾ß ÇÔ
        resultPanel = GameObject.Find("GameresultPanel");
        resultPanel.gameObject.SetActive(false);
        answerPanel = GameObject.Find("AnswerPanel");
        word = GameObject.Find("HangmanWord").GetComponent<Word>();
        SelectWordPanel = GameObject.Find("SelectWordPanel");
        SelectWordPanel.gameObject.SetActive(false);
        resultPanel.gameObject.SetActive(false);
        txt = word.GetComponent<TextMeshProUGUI>();
    }
    public void get_word(string Character,int Count)
    {
        string threeword;
        Destroy(GameObject.Find("WaitPanel"));
        SetCharacter(Character);
        SelectWordPanel.gameObject.SetActive(true);
        StartCoroutine(GameObject.Find("network_manager(Clone)").GetComponent<Network_manager>().GetThreeWord(Count, (Message) => {
            threeword = Message;
            Debug.Log(threeword);
            GetWord(threeword);
            GameObject.Find("ui_manager(Clone)").GetComponent<ui_manager>().ThreeWordButton();
        }));
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
    public void SelectWord(string selectedword)
    {
        word.curWord = selectedword.ToUpper();
        curWord = word.curWord;
        StartCoroutine(GameObject.Find("network_manager(Clone)").GetComponent<Network_manager>().GetMean(curWord, (Message) =>
         {
             MakeRoomMessage mean_temp;
             mean_temp = JsonUtility.FromJson<MakeRoomMessage>(Message);
             mean = mean_temp.result;
             Debug.Log("Word Mean :" + mean);
         }));
        var arr = curWord.ToCharArray();
        for(int i = 0; i < curWord.Length; i++)
        {
            hintWord.Add(arr[i]);
        }
        Debug.Log("now Word :"+word.curWord);
        
        for (int i = 0; i < word.curWord.Length; i++)
        {
            reponse += "_";
        }
        Debug.Log(reponse);
        txt.text = reponse;
    }
    public int DiceStart()
    {
        MyDiceani = GameObject.Find("MyDice");
        int Result = Random.Range(0, 5);
        StartCoroutine(RollTheDice(MyDiceani.GetComponent<Image>(), Result));
        return Result+1;
    }
    public void OtherDiceStart(int Result)
    {
        Debug.Log("otherdice:" + Result);
        OtherDiceani = GameObject.Find("OtherDice");
        StartCoroutine(RollTheDice(OtherDiceani.GetComponent<Image>(), Result-1));
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
    public IEnumerator closedicepanel()
    {
        yield return new WaitForSeconds(4f);
        GameObject.Find("DicePanel").SetActive(false);
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
            Destroy(NotMyTurn_instance);
        }

    }
    public void OtherDeathcount()
    {
        otherplayer_death_count++;
    }
    public void KeyboardPress(string letter)
    {
        GameObject.Find("network_manager(Clone)").GetComponent<Network_manager>().SendPlayMessage(Validation(letter));
        TurnStart("other");
        Debug.Log("Validation  "+letter+"\n"+ word.curWord.Length);
    }
    private bool Validation(string letter)
    {
        letter = letter.ToUpper();
        reponse = "";
        win = false;
        bool check=false;
        for(int i = 0; i < word.curWord.Length; i++)
        {
            if (txt.text.Substring(i, 1) == "_")
            {
                if (word.curWord.Substring(i, 1) == letter)
                {
                    hintWord.RemoveAt(hintWord.IndexOf(char.Parse(letter)));
                    reponse += letter;
                    win = true;
                    check = true;
                    GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().PlaySFXSound("GoodWork");
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
        if (win == false)
        {
            GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().PlaySFXSound("No");
        }
        txt.text = reponse;
        Verification();
        return check;
    }
    public char UseHint()
    {
        return hintWord[0];
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
    public void Direct_Verification(string answer)
    {
        answer = answer.ToUpper();
        if (answer==word.curWord)
         {
             GameResult(true);
         }
        else
        {
            GameResult(false);
        }
    }
    public void OtherVerification(int result)
    {
        if (result == -1)
        {
            OtherDeathcount();
            if (otherplayer_death_count == 10)
            {
                //GameObject.Find("network_manager(Clone)").GetComponent<Network_manager>().SendResultMessage(Validation(letter));
                GameResult(true);
            }
            else
            {
                Enemycharacter.GetComponent<Image>().sprite = now_sprites[otherplayer_death_count];
                TurnStart("my");
            }
        }
        else if (result == 1)
        {
            TurnStart("my");
        }
    }
    public void SetActiveAnswer()
    {
        answerPanel.SetActive(true);
    }
    public void GameResult(bool check)
    {
        Destroy(NotMyTurn_instance);
        if (check == true&& resultPanel.activeInHierarchy ==false) {
            answerPanel.SetActive(false);
            resultPanel.SetActive(true);
            resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You Win Your Word is "+word.curWord+System.Environment.NewLine+"mean:"+mean;
            GameObject.Find("network_manager(Clone)").GetComponent<Network_manager>().SendGameResultMessage(true);
            GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().PlaySFXSound("YouWin");
        }
        else if(check == false&&resultPanel.activeInHierarchy == false)
        {
            answerPanel.SetActive(false);
            resultPanel.SetActive(true);
            resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You Lose Your Word is " + word.curWord + System.Environment.NewLine + "mean:" + mean;
            GameObject.Find("network_manager(Clone)").GetComponent<Network_manager>().SendGameResultMessage(false);
            GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().PlaySFXSound("YouLose");
        }
    }
}
