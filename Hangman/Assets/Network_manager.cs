using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.Networking;
using TMPro;


public class MemberData : LoginData
{
    public string nickname;
}
public class LoginData
{
    public string id;
    public string pw;
}
public class UserData : MemberData
{
    public string User_win;
    public string User_lose;
}

public class Network_manager  : MonoBehaviour
{
    public string Url;
    public string Message;
    public TextMeshProUGUI User_id;
    public TextMeshProUGUI User_password;
    public TextMeshProUGUI User_nickname;

    public TextMeshProUGUI Login_id;
    public TextMeshProUGUI Login_password;
    private void Awake() { DontDestroyOnLoad(gameObject); }
    public void LoginData()
    {
        LoginData login_data = new LoginData();
        
        string json = JsonUtility.ToJson(login_data);
        StartCoroutine(Upload("http://52.79.234.207:8080/sign-in", json));
    }
    public void SignupData()
    {
        MemberData data = new MemberData();
        data.id = User_id.text;
        data.nickname = User_nickname.text;
        data.pw = User_password.text;
        string json = JsonUtility.ToJson(data);
        StartCoroutine(Upload("http://3.35.3.123:8080/sign-up", json));
    }
    public void IdCheckData()
    {
        MemberData data = new MemberData();
       
        string json = JsonUtility.ToJson(data);
        StartCoroutine(Upload("http://52.79.234.207:8080/sign-in", json));
    }
    public void NicknameCheckData()
    {
        MemberData data = new MemberData();
        string json = JsonUtility.ToJson(data);
        StartCoroutine(Upload("http://52.79.234.207:8080/sign-in", json));
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
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}
