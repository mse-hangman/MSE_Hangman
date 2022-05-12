using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sign_up : MonoBehaviour
{
    public GameObject Network_manager;
    public TextMeshProUGUI User_id;
    public TMP_InputField User_password;
    public TMP_InputField User_password_check;
    public TextMeshProUGUI User_nickname;

    private TextMeshProUGUI popup_text;

    private bool check = false;
    private void Start()
    {
        popup_text = Network_manager.gameObject.GetComponent<Network_manager>().Pop_up.GetComponentInChildren<TextMeshProUGUI>();
        check = true;
    }
    public void SignupButton()
    {
        if (User_password== User_password_check)
        {
            check = true;
        }

        if (check == true)
            {
            Network_manager.gameObject.GetComponent<Network_manager>().SignupData(User_id.text, User_password.text, User_nickname.text);
            Network_manager.gameObject.GetComponent<Network_manager>().Pop_up.SetActive(true);
        }
        else if(check == false)
        {
            popup_text.text = "Please check input again";
            Network_manager.gameObject.GetComponent<Network_manager>().Pop_up.SetActive(true);
        }
    }
    public void CheckIdButton()
    {
       Network_manager.gameObject.GetComponent<Network_manager>().IdCheckData(User_id.text);
    }
    public void CheckNickButton()
    {
        Network_manager.gameObject.GetComponent<Network_manager>().NicknameCheckData(User_nickname.text);
    }
}
