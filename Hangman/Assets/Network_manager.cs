using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.Networking;
using TMPro;


public class MemberData
{
    public string User_id;
    public string User_password;
    public string User_nickname;

}

public class Network_manager  : MonoBehaviour
{
    public string Url;
    public string Message;
    public TextMeshProUGUI User_id;
    public TextMeshProUGUI User_password;
    public TextMeshProUGUI User_nickname;
    private void Awake() { DontDestroyOnLoad(gameObject); }
    public void SendMemberData()
    {
        MemberData data = new MemberData();
        data.User_id = User_id.text;
        data.User_nickname = User_nickname.text;
        data.User_password = User_password.text;
        string json = JsonUtility.ToJson(data);
        StartCoroutine(Upload("http://localhost/hellomvc/unitymapping", json));
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
