using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using WebSocketSharp;

[Serializable]
public class MakeRoomMessage
{
  int statusCode;
  string result;
    public string get_result()
    {
        return result;
    }
}


public class MemberData : LoginData
{
    public string user_nickname;
}
public class LoginData
{
    public string user_id;
    public string user_pw;
}
public class UserData : MemberData
{
    public string user_win;
    public string user_lose;
}
public class requestFormat
{
    public string type;
    public int gameroomId;
}
public class startFormat : requestFormat
{
    public string userId;
}
public class emojiFormat
{
    public string type="EMOJI";
    public int gameroomId;
    public int emoji;
}
public class DiceFormat
{
    public string type = "TURN";
    public int gameroomId;
    public int diceNumber;
}
public class WordFormat
{
    public string type = "WORD";
    public int gameroomId;
    public string wordForCounterpart;
}
public class PlayFormat
{
    public string type = "PLAY";
    public int gameroomId;
    public bool isCorrect;
}
public class Room
{
    public string title;
    public int wordCount;
    public string gameCharacter;
}


public class Network_manager : MonoBehaviour
{
    public string Url;
    public string Message;
    private string Downloaddata;
    private bool nickname_check = false, id_check = false;
    public int RoomId;

    public WebSocketSharp.WebSocket Socket = null;

    public GameObject Pop_up;
    private MemberData data;
    private LoginData login_data;
    public emojiFormat Emoji;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        data = new MemberData();
        login_data = new LoginData();
        Emoji = new emojiFormat();
        // ws://13.125.205.198:8080/gameroom/{gameroom}
    }
    public IEnumerator LoginData(string User_id, string User_password, Action<string> callback)
    {
        Debug.Log(User_id + User_password);
        login_data.user_id = User_id;
        login_data.user_pw = User_password;
        string json = JsonUtility.ToJson(login_data);
        StartCoroutine(Upload("http://13.125.205.198:8080/log-in", json, (request) => {
            Debug.Log(Message);
            callback(Message);
        }));
        yield return null;
    }

    public IEnumerator SignupData(string User_id, string User_password, string User_nickname, Action<string> callback)
    {

        data.user_id = User_id;
        data.user_nickname = User_nickname;
        data.user_pw = User_password;
        string json = JsonUtility.ToJson(data);
        StartCoroutine(Upload("http://13.125.205.198:8080/sign-up", json, (request) => {
            Debug.Log(Message);
            callback(Message);
        }));
        yield return null;
    }
    public IEnumerator IdCheckData(string User_id, Action<string> callback)
    {
        data.user_id = User_id;
        string json = JsonUtility.ToJson(data);
        StartCoroutine(Upload("http://13.125.205.198:8080/checkID", json, (request) => {
            if (Message == "Valid ID")
                id_check = true;
            Debug.Log(id_check);
            callback(Message);
        }));
        yield return null;

    }
    public IEnumerator NicknameCheckData(string User_nickname, Action<string> callback)
    {
        data.user_nickname = User_nickname;
        string json = JsonUtility.ToJson(data);
        StartCoroutine(Upload("http://13.125.205.198:8080/checkNN", json, (request) => {
            if (Message == "Valid Nickname")
                nickname_check = true;
            Debug.Log(nickname_check);
            callback(Message);
        }));
        yield return null;
    }
    public IEnumerator MakeRoom(string title, string wordCount, string gameCharacter, Action<string> callback)
    {
        Room new_room = new Room();
        new_room.title = title;
        new_room.wordCount = int.Parse(wordCount);
        new_room.gameCharacter = gameCharacter;
        string json = JsonUtility.ToJson(new_room);
        Debug.Log(json);
        StartCoroutine(Upload("http://13.125.205.198:8080/gameroom", json, (request) => {
           // RoomId = int.Parse(Message);

            callback(Message);
        }));
        yield return null;
    }
    public bool signup_check()
    {
        if (id_check == true && nickname_check == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public IEnumerator GetThreeWord(int wordcount,Action<string> callback)
    {
        StartCoroutine(Download("http://13.125.205.198:8080/word/random?wordCount="+wordcount, (request) => {
            Debug.Log(Downloaddata);
            callback(Downloaddata);
        }));
        yield return 0;
    }
    public IEnumerator GetRoomList(Action<string> callback)
    {
        StartCoroutine(Download("http://13.125.205.198:8080/gameroom", (request) => {
            Debug.Log(Downloaddata);
            callback(Downloaddata);
        }));
        yield return 0;
    }

    IEnumerator Upload(string URL, string json, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(URL, json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                Debug.Log(json);
            }
            else
            {
                Message = request.downloadHandler.text;
                Debug.Log(json);
                Debug.Log(Message);
            }
            callback(request);
        }
    }

    IEnumerator Download(string URL, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            Debug.Log("download");
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                Downloaddata = request.downloadHandler.text;
            }
            callback(request);
        }
    }

    public void Connect(string SERVICE_NAME)
    {
        try {
            if (Socket == null || !Socket.IsAlive)
                try
                {
                    // ws://13.125.205.198:8080/gameroom/{gameroom}
                    Socket = new WebSocketSharp.WebSocket("ws://13.125.205.198:8080/gameroom/" + SERVICE_NAME);
                    Socket.OnMessage += Recv;
                    Socket.OnClose += CloseeConnect;
                    RoomId = int.Parse(SERVICE_NAME);
                }
                catch { }
            Socket.ConnectAsync();
            SendSocketMessage("CHECK", RoomId);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    public void CloseeConnect(object sender, CloseEventArgs e)
    {
        Debug.Log(e.Reason);
        DisconnectServer();
    }

    public void DisconnectServer()
    {
        try
        {
            if (Socket == null)
                return;
            if (Socket.IsAlive)
                Socket.Close();
        }catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    public void SendSocketMessage(string type,int gameroomId)
    {
        requestFormat format = new requestFormat();
        format.gameroomId = gameroomId;
        format.type = type;
        
        string json = JsonUtility.ToJson(format);
        Debug.Log(json);
        SendJsonSocket(json);
    }
    public void SendEmojiMessage(int emoji)
    {
        emojiFormat format = new emojiFormat();
        format.emoji = emoji;
        format.gameroomId = RoomId;
        string json = JsonUtility.ToJson(format);
        Debug.Log(json);
        SendJsonSocket(json);
    }
    public void SendDiceMessage(int Dice)
    {
        DiceFormat format = new DiceFormat();
        format.diceNumber = Dice;
        format.gameroomId = RoomId;
        string json = JsonUtility.ToJson(format);
        Debug.Log(json);
        SendJsonSocket(json);
    }
    public void SendWordMessage(string word)
    {
        WordFormat format = new WordFormat();
        format.wordForCounterpart = word;
        format.gameroomId = RoomId;
        string json = JsonUtility.ToJson(format);
        Debug.Log(json);
        SendJsonSocket(json);
    }
    public void SendPlayMessage(bool message)
    {
        PlayFormat format = new PlayFormat();
        format.gameroomId = RoomId;
        format.isCorrect = message;

        string json = JsonUtility.ToJson(format);
        Debug.Log(json);
        SendJsonSocket(json);
    }
    public void SendStartMessage()
    {
        startFormat format = new startFormat();
        format.type = "START";
        format.gameroomId = RoomId;
        format.userId = login_data.user_id;
        string json = JsonUtility.ToJson(format);
        Debug.Log(json);
        SendJsonSocket(json);
    }
    public void SendJsonSocket(string json)
    {
        if (!Socket.IsAlive)
            return;
        try
        {
            Socket.Send(json);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public string GetDownloaddata()
    {
        return Downloaddata;
    }
    public void Recv(object sender, MessageEventArgs e)
    {
        Debug.Log(e.Data);
        Debug.Log("NOPE");//보내는거랑 받는거랑 같이 할때 에러 발생
        Debug.Log(e.RawData);
        
        if (e.Data.Contains("EMOJI"))
        {
            Emoji = JsonUtility.FromJson<emojiFormat>(e.Data);
            StartCoroutine(GameObject.Find("EmojiManager").GetComponent<Emoji_Manger>().OtherPlayerEmoji(Emoji.emoji));
        }
        if (e.Data== "Players are ready. Choose the word!")
        {

        }
        if (e.Data == "Word matching is finished!")
        {
            SendDiceMessage(GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().DiceStart());
        }
        if (e.Data == data.user_id+" goes first")
        {
            Debug.Log("RIGHT");
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().TurnStart("my");
        }
        else if (e.Data.Contains(" goes first"))
        {
            GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().TurnStart("other");
        }
        if (e.Data == "DRAW. Roll the dice again")
        {
            SendDiceMessage(GameObject.Find("HangmanManger(Clone)").GetComponent<hangman_manager>().DiceStart());
        }
    }

}




