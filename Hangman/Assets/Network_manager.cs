using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;


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
    public string User_lose;
}

public class Network_manager  : MonoBehaviour
{
    public string Url;
    public string Message;
    private string Downloaddata;

    public GameObject Pop_up;
    private MemberData data;


    private void Awake() { DontDestroyOnLoad(gameObject); }
    private void Start()
    {
        data = new MemberData();
    }
    public void LoginData(string User_id, string User_password)
    {
        Debug.Log(User_id + User_password);
        LoginData login_data = new LoginData();
        login_data.user_id = User_id;
        login_data.user_pw = User_password;
        string json = JsonUtility.ToJson(login_data);
        StartCoroutine(Upload("http://3.35.3.123:8080/log-in", json));
    }
    public void SignupData(string User_id,string User_password,string User_nickname)
    {
        
        data.user_id = User_id;
        data.user_nickname = User_nickname;
        data.user_pw = User_password;
        string json = JsonUtility.ToJson(data);
        StartCoroutine(Upload("http://3.35.3.123:8080/sign-up", json));
    }
    public void IdCheckData(string User_id)
    {
        data.user_id = User_id;
        string json = JsonUtility.ToJson(data);
        StartCoroutine(Upload("http://52.79.234.207:8080/sign-in", json));

    }
    public void NicknameCheckData(string User_nickname)
    {
        data.user_nickname = User_nickname;
        string json = JsonUtility.ToJson(data);
        StartCoroutine(Upload("http://52.79.234.207:8080/sign-in", json));
    }
    public void GetThreeWord()
    {
        StartCoroutine(Download("http://3.35.3.123:8080/word/random?wordCount=5"));
    }

    IEnumerator Upload(string URL,string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(URL, json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if(request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                Debug.Log(json);
            }
            else
            {
                Message = request.downloadHandler.text;
                if (Message == "login success")
                {
                    MoveToLobby();
                }
                else
                {
                    Pop_up.GetComponentInChildren<TextMeshProUGUI>().text = Message;
                    Pop_up.SetActive(true);
                }
                Debug.Log(json);
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
    IEnumerator Download(string URL)
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
        }
    }

    public void close_popup()
    {
        Pop_up.SetActive(false);
    }
    public void MoveToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public string GetDownloaddata()
    {
        return Downloaddata;
    }
}
