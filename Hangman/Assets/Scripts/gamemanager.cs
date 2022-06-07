using System.Collections;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class gamemanager : MonoBehaviour
{
    List<string> getter;

    private string message;

    public GameObject network_manager;
    public GameObject ui_manager;
    public GameObject hangman_manager;
    public GameObject emoji_manager;
    public GameObject waiting_manager_instance;
    public GameObject waiting_manager;
    public Room_List myroom;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        network_manager=Instantiate(network_manager);
        ui_manager = Instantiate(ui_manager);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SetResolution();
        myroom = new Room_List();

        getter = new List<string>();
        ui_manager.GetComponent<ui_manager>().SignupListner();
    }
    private void SetResolution()
    {
        int setWidth = 1920;
        int setHeight = 1080;

        Screen.SetResolution(setWidth, setHeight, false);
    }
    public void log_in()
    {
        getter=ui_manager.GetComponent<ui_manager>().LogInButton();
        StartCoroutine(network_manager.GetComponent<Network_manager>().LoginData(getter[0], getter[1],(Message)=> {
            message = Message;
            Debug.Log(message);
            if (message == "login fail")//테스트를 위해 바꿈
            {
                SceneManager.LoadScene("LobbyScene");
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
    // -------------------------------login------------------------------------------------
    public void make_room()//myroom 정보 저장해야함
    {
        getter = ui_manager.GetComponent<ui_manager>().MakeRoomButton();
        myroom.wordCount = int.Parse(getter[1]);
        myroom.gameCharacter = getter[2];
        StartCoroutine(network_manager.GetComponent<Network_manager>().MakeRoom(getter[0], getter[1], getter[2], (Message) =>
         {
                message = Message;
                Debug.Log(message.Length);
                string id_temp = message.Substring(28,43-28);
                Debug.Log(id_temp);
                myroom.gameroomId = int.Parse(Regex.Replace(id_temp, @"\D", ""));

                //Debug.Log("room id :" + myroom.gameroomId + "room character :" + myroom.gameCharacter + "wordcount :" + myroom.wordCount);
             if (message !=null)
             {
                 SceneManager.LoadScene("WaitingRoomScene");
             }
            }));
        getter.Clear();
    }

    public void get_roominformation(int index)
    {
        GameObject myroom_temp = GameObject.Find("RoomListManager").GetComponent<RoomListmanager>().getRoomList(index);
        myroom.gameroomId = int.Parse(myroom_temp.GetComponent<RoomComponent>().gameroomId.text.ToString());
        myroom.gameCharacter = myroom_temp.GetComponent<RoomComponent>().gameCharacter.text.ToString();
        myroom.wordCount = int.Parse(myroom_temp.GetComponent<RoomComponent>().wordCount.text.ToString());
        Debug.Log("room id :" + myroom.gameroomId + "room character :" + myroom.gameCharacter + "wordcount :" + myroom.wordCount);
    }
    public void enter_room(int index)
    {
        Debug.Log("index"+index);
        get_roominformation(index);
        SceneManager.LoadScene("WaitingRoomScene");
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name);
        if (scene.name == "LobbyScene")
        {
            get_roomlist();
            ui_manager.GetComponent<ui_manager>().MakeRoom();
        }
        if (scene.name=="WaitingRoomScene")
        {
            waiting_manager_instance = Instantiate(waiting_manager);
            waiting_manager_instance.GetComponent<WaitingRoom_Manager>().setcharacter(myroom.gameCharacter);
            Debug.Log("myroomid is "+ myroom.gameroomId);
            network_manager.GetComponent<Network_manager>().Connect(myroom.gameroomId.ToString());//room 입력 부분
           emoji_manager = GameObject.Find("EmojiManager");
            ui_manager.GetComponent<ui_manager>().StartGame();
        }
        if (scene.name == "GameScene")
        {
            hangman_manager = Instantiate(hangman_manager);
            ui_manager.GetComponent<ui_manager>().HangmanButtonSet();
            get_word();
        }
    }
    public void start_game()
    {
        SceneManager.LoadScene("GameScene");
        network_manager.GetComponent<Network_manager>().SendStartMessage();
    }
    public void get_word()
    {
        string threeword;
        hangman_manager.GetComponent<hangman_manager>().SetCharacter(myroom.gameCharacter);
        StartCoroutine(network_manager.GetComponent<Network_manager>().GetThreeWord(myroom.wordCount,(Message) => {
            threeword = Message;
            Debug.Log(threeword);
            hangman_manager.GetComponent<hangman_manager>().GetWord(threeword);
            ui_manager.GetComponent<ui_manager>().ThreeWordButton();
        }));
    }
    public void get_roomlist()
    {
        StartCoroutine(network_manager.GetComponent<Network_manager>().GetRoomList((Message) => {
            Room_List[] rooms = JsonHelper.FromJson<Room_List>("{\"Items\":" + Message + "}");
            GameObject.Find("RoomListManager").GetComponent<RoomListmanager>().MakeRoom(rooms);
        }));
    }
    public void select_word(int index)
    {
        List<string> word = GameObject.Find("HangmanWord").GetComponent<Word>().getThreeWord();
        hangman_manager.GetComponent<hangman_manager>().SelectWord(index);
        network_manager.GetComponent<Network_manager>().SendWordMessage(word[index]);
    }
    public void hangman_start()
    {
        
    }
}
