using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using TMPro;

public class Sign_in : MonoBehaviour
{
    public TextMeshProUGUI User_id, User_password;
    // Update is called once per frame

    public void Log_in()
    {
        Debug.Log(User_id.text+User_password.text);
    }
   
}
