using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ui_manager : MonoBehaviour
{
    public Button firstword;
    public Button secondword;
    public Button thirdword;

    public Button makeroombutton;

    public Button[] HangmanButton = new Button[26];

    public GameObject Popup;
    public TextMeshProUGUI Popup_text;

    public TMP_InputField Login_password;
    public TMP_InputField Login_id;

    public TMP_InputField Signup_id;
    public TMP_InputField Signup_password;
    public TMP_InputField Signup_repassword;
    public TMP_InputField Signup_nickname;

    public TMP_InputField MakeRoom_title;
    public TextMeshProUGUI MakeRoom_wordcount;
    public Dropdown MakeRoom_Graphic;

    private List<string> getter;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        getter = new List<string>();
        Login_id = GameObject.Find("login_id_input").GetComponent<TMP_InputField>();
        Login_password = GameObject.Find("login_password_input").GetComponent<TMP_InputField>();
    }

    public void Signup()
    {
        Signup_id = GameObject.Find("signup_id_input").GetComponent<TMP_InputField>();
        Signup_password = GameObject.Find("signup_password_input").GetComponent<TMP_InputField>();
        Signup_repassword = GameObject.Find("signup_repassword_input").GetComponent<TMP_InputField>();
        Signup_nickname = GameObject.Find("signup_nickname_input").GetComponent<TMP_InputField>();
    }
    public void CloseSignup()
    {
        GameObject.Find("SignupPanel").gameObject.SetActive(false);
    }
    public void SignupListner()
    {
        makeroombutton = GameObject.Find("Sign up").GetComponent<Button>();
        makeroombutton.onClick.AddListener(() =>
        {
            Signup();
        });
    }
    public void MakeRoom()
    {
        makeroombutton = GameObject.Find("MakeRoom Button").GetComponent<Button>();
        makeroombutton.onClick.AddListener(() =>
        {
            GameObject.Find("GameManger").GetComponent<gamemanager>().make_room();
        });
        MakeRoom_title = GameObject.Find("RoomTitle").GetComponent<TMP_InputField>();
        MakeRoom_wordcount = GameObject.Find("WordCount").GetComponent<TextMeshProUGUI>();
        MakeRoom_Graphic = GameObject.Find("CharacterDropdown").GetComponent<Dropdown>();
        GameObject.Find("MakeRoomPanel").SetActive(false);
    }

    public void StartGame()
    {
        makeroombutton = GameObject.Find("GameStart Button").GetComponent<Button>();
        
        makeroombutton.onClick.AddListener(() =>
        {
            GameObject.Find("GameManger").GetComponent<gamemanager>().start_game();
        });
    }

    // Start is called before the first frame update
    public List<string> LogInButton()
    {
        getter.Clear();
        getter.Add(Login_id.text);
        getter.Add(Login_password.text);
        return getter;
    }
    public List<string> SignupButton()
    {
        getter.Clear();
        getter.Add(Signup_id.text);
        getter.Add(Signup_password.text);
        getter.Add(Signup_nickname.text);
        return getter;
    }
    public List<string> IdCheckButton()
    {
        getter.Clear();
        getter.Add(Signup_id.text);
        return getter;
    }
    public List<string> NicknameCheckButton()
    {
        getter.Clear();
        getter.Add(Signup_nickname.text);
        return getter;
    }
    public void PopUPText(string message)
    {
        Popup = GameObject.Find("PopUpPanel");
        Popup_text = GameObject.Find("PopUpText").GetComponent<TextMeshProUGUI>();
        Popup_text.text = message;
        Popup.SetActive(true);
    }
    public bool RemindPassword()
    {
        if (Signup_password.text == Signup_repassword.text)
        {
            Debug.Log("true");
            return true;
        }
        else
            return false;
    }
    public List<string> MakeRoomButton()
    {
        //글자 최소수 3, 최대 수 5
        getter.Clear();
        getter.Add(MakeRoom_title.text);
        getter.Add(MakeRoom_wordcount.text);
        getter.Add(MakeRoom_Graphic.options[MakeRoom_Graphic.value].text);
        return getter;
    }
    public void ThreeWordButton( )
    {
        List<string> word = GameObject.Find("HangmanWord").GetComponent<Word>().getThreeWord();

        GameObject.Find("1stword").GetComponentInChildren<TextMeshProUGUI>().text = word[0];
        GameObject.Find("2ndword").GetComponentInChildren<TextMeshProUGUI>().text = word[1];
        GameObject.Find("3rdword").GetComponentInChildren<TextMeshProUGUI>().text = word[2];
        firstword = GameObject.Find("1stword").GetComponent<Button>();
        secondword = GameObject.Find("2ndword").GetComponent<Button>();
        thirdword = GameObject.Find("3rdword").GetComponent<Button>();
        firstword.onClick.AddListener(() =>
        {
            GameObject.Find("GameManger").GetComponent<gamemanager>().select_word(0);
        });
        secondword.onClick.AddListener(() =>
        {
            GameObject.Find("GameManger").GetComponent<gamemanager>().select_word(1);
        });
        thirdword.onClick.AddListener(() =>
        {
            GameObject.Find("GameManger").GetComponent<gamemanager>().select_word(2);
        });
    }
    public void HangmanButtonSet()
    {
        int count = 0;
        HangmanButton[count] = GameObject.Find("A").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("A");
        });
        count++;
        HangmanButton[count] = GameObject.Find("B").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("B");
        });
        count++;
        HangmanButton[count] = GameObject.Find("C").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("C");
        });
        count++;
        HangmanButton[count] = GameObject.Find("D").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("D");
        });
        count++;
        HangmanButton[count] = GameObject.Find("E").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("E");
        });
        count++;
        HangmanButton[count] = GameObject.Find("F").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("F");
        });
        count++;
        HangmanButton[count] = GameObject.Find("G").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("G");
        });
        count++;
        HangmanButton[count] = GameObject.Find("H").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("H");
        });
        count++;
        HangmanButton[count] = GameObject.Find("I").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("I");
        });
        count++;
        HangmanButton[count] = GameObject.Find("J").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("J");
        });
        count++;
        HangmanButton[count] = GameObject.Find("K").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("K");
        });
        count++;
        HangmanButton[count] = GameObject.Find("L").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("L");
        });
        count++;
        HangmanButton[count] = GameObject.Find("M").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("M");
        });
        count++;
        HangmanButton[count] = GameObject.Find("N").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("N");
        });
        count++;
        HangmanButton[count] = GameObject.Find("O").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("O");
        });
        count++; 
        HangmanButton[count] = GameObject.Find("P").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("P");
        });
        count++;
        HangmanButton[count] = GameObject.Find("Q").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("Q");
        });
        count++; 
        HangmanButton[count] = GameObject.Find("R").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("R");
        });
        count++; 
        HangmanButton[count] = GameObject.Find("S").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("S");
        });
        count++; 
        HangmanButton[count] = GameObject.Find("T").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("T");
        });
        count++; 
        HangmanButton[count] = GameObject.Find("U").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("U");
        });
        count++; 
        HangmanButton[count] = GameObject.Find("V").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("V");
        });
        count++; 
        HangmanButton[count] = GameObject.Find("W").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("W");
        }); 
        count++;
        HangmanButton[count] = GameObject.Find("X").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("X");
        });
        count++;
        HangmanButton[count] = GameObject.Find("Y").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("Y");
        });
        count++;
        HangmanButton[count] = GameObject.Find("Z").GetComponent<Button>();
        HangmanButton[count].onClick.AddListener(() =>
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().KeyboardPress("Z");
        });
    }
}
