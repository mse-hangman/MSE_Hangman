using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private AudioSource BGMPlayer;
    private AudioSource sfxPlayer;

    [SerializeField]
    public AudioClip[] sfxAudioClips; //ȿ������ ����

    public AudioClip StartBGM; //ȿ������ ����
    public AudioClip LobbyBGM; //ȿ������ ����
    public AudioClip GameBGM; //ȿ������ ����


    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;

    Dictionary<string, AudioClip> audioClipsDic = new Dictionary<string, AudioClip>(); //ȿ���� ��ųʸ�

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); //���� ������ ����� ��.

        BGMPlayer = GameObject.Find("BGMSoundPlayer").GetComponent<AudioSource>();
        sfxPlayer = GameObject.Find("SFXSoundPlayer").GetComponent<AudioSource>();

        foreach (AudioClip audioclip in sfxAudioClips)
        {
            audioClipsDic.Add(audioclip.name, audioclip);
        }
    }

    // ȿ�� ���� ��� : �̸��� �ʼ� �Ű�����, ������ ������ �Ű������� ����
    public void PlaySFXSound(string name, float volume = 1f)
    {
        if (audioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipsDic[name], volume * masterVolumeSFX);
    }

    //BGM ���� ��� : ������ ������ �Ű������� ����
    public void PlayBGMSound(float volume = 0.5f)
    {
        BGMPlayer.loop = true; //BGM �����̹Ƿ� ��������
        BGMPlayer.volume = volume * masterVolumeBGM;

        if (SceneManager.GetActiveScene().name == "LoginScene")
        {
            BGMPlayer.clip = StartBGM;
            BGMPlayer.Play();
        }
        if (SceneManager.GetActiveScene().name == "LobbyScene")
        {
            BGMPlayer.clip = LobbyBGM;
            BGMPlayer.Play();
        }
        else if (SceneManager.GetActiveScene().name == "GameScene")
        {
            BGMPlayer.clip = GameBGM;
            BGMPlayer.Play();
        }
        //���� ���� �´� BGM ���
    }

}
