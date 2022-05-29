using System.Collections;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    List<string> getter;

    private string message;

    public GameObject network_manager;
    public GameObject ui_manager;
    public GameObject hangman_manager;
    public GameObject emoji_manager;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        network_manager=Instantiate(network_manager);
        ui_manager = Instantiate(ui_manager);
        SceneManager.sceneLoaded += OnSceneLoaded;


        getter = new List<string>();
        ui_manager.GetComponent<ui_manager>().SignupListner();
    }
    public void log_in()
    {
        getter=ui_manager.GetComponent<ui_manager>().LogInButton();
        StartCoroutine(network_manager.GetComponent<Network_manager>().LoginData(getter[0], getter[1],(Message)=> {
            message = Message;
            Debug.Log(message);
            if (message == "login fail")//test를위해 잠시 바꿈
            {
                SceneManager.LoadScene("LobbyScene");
                StartCoroutine(ui_manager.GetComponent<ui_manager>().MakeRoom());
            }
            else
            {
                ui_manager.GetComponent<ui_manager>().PopUPText(message);
            }
        } ));
        
        getter.Clear();
    }
    public void sign_in()
    {
        if (network_manager.GetComponent<Network_manager>().signup_check() && ui_manager.GetComponent<ui_manager>().RemindPassword())
        {
            getter = ui_manager.GetComponent<ui_manager>().SignupButton();
            StartCoroutine(network_manager.GetComponent<Network_manager>().SignupData(getter[0], getter[1], getter[2], (Message) =>
            {
                message = Message;
                Debug.Log(message);
                ui_manager.GetComponent<ui_manager>().PopUPText(message);
                if (message == "sign-up success")
                    ui_manager.GetComponent<ui_manager>().CloseSignup();
            }));
        }
        else
        {
            if(!network_manager.GetComponent<Network_manager>().signup_check())
                ui_manager.GetComponent<ui_manager>().PopUPText("Please Duplicate check Id, Nickname ");
            else if (!ui_manager.GetComponent<ui_manager>().RemindPassword())
                ui_manager.GetComponent<ui_manager>().PopUPText("Please Check Your Password");
            else
            {
                ui_manager.GetComponent<ui_manager>().PopUPText("Please Check your Id, Nickname,Password");
            }
        }
        getter.Clear();
    }
    public void id_check()
    {
        getter = ui_manager.GetComponent<ui_manager>().IdCheckButton();
        StartCoroutine(network_manager.GetComponent<Network_manager>().IdCheckData(getter[0], (Message) => {
            message = Message;
            Debug.Log(message);
            ui_manager.GetComponent<ui_manager>().PopUPText(message);
        }));
        getter.Clear();
    }
    public void nick_check()
    {
        getter = ui_manager.GetComponent<ui_manager>().NicknameCheckButton();
        StartCoroutine(network_manager.GetComponent<Network_manager>().NicknameCheckData(getter[0], (Message) => {
            message = Message;
            Debug.Log(message);
            ui_manager.GetComponent<ui_manager>().PopUPText(message);
        }));
        getter.Clear();
    }
    public void make_room()
    {
        getter = ui_manager.GetComponent<ui_manager>().MakeRoomButton();
        StartCoroutine(network_manager.GetComponent<Network_manager>().MakeRoom(getter[0], getter[1], getter[2], (Message) =>
         {
                message = Message;
                Debug.Log(message);
         if (message !=null)
             {
                 SceneManager.LoadScene("WaitingRoomScene");
                 
                
                
             }
                //ui_manager.GetComponent<ui_manager>().PopUPText(message);
            }));
        getter.Clear();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name);
        if (scene.name=="WaitingRoomScene")
        {
            network_manager.GetComponent<Network_manager>().Connect("1");
            emoji_manager = GameObject.Find("EmojiManager");
            ui_manager.GetComponent<ui_manager>().StartGame();
        }
        if (scene.name == "GameScene")
        {
            hangman_manager = Instantiate(hangman_manager);
            get_word();
        }
    }
    public void start_game()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void get_word()
    {
        string threeword;
        Debug.Log("start");
        StartCoroutine(network_manager.GetComponent<Network_manager>().GetThreeWord(3,(Message) => {
            threeword = Message;
            Debug.Log(threeword);
            hangman_manager.GetComponent<hangman_manager>().GetWord(threeword);
            ui_manager.GetComponent<ui_manager>().ThreeWordButton();
        }));
    }
    public void select_word(int index)
    {
        List<string> word = GameObject.Find("HangmanWord").GetComponent<Word>().getThreeWord();
        hangman_manager.GetComponent<hangman_manager>().SelectWord(index);
        network_manager.GetComponent<Network_manager>().SendWordMessage(word[index]);
        hangman_manager.GetComponent<hangman_manager>().DiceStart();
    }
    public void hangman_start()
    {

    }
}
