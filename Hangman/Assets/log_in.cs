using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class log_in : MonoBehaviour
{
    public GameObject Network_manager;

    public TMP_InputField Login_password;
    public TextMeshProUGUI Login_id;
    // Start is called before the first frame update
    public void LogInButton()
    {
        Network_manager.gameObject.GetComponent<Network_manager>().LoginData(Login_id.text, Login_password.text);
    }

}
