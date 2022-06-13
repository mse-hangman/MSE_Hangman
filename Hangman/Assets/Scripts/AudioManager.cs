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
    public AudioClip[] sfxAudioClips; //효과음들 지정

    public AudioClip StartBGM; //효과음들 지정
    public AudioClip LobbyBGM; //효과음들 지정
    public AudioClip GameBGM; //효과음들 지정


    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;

    Dictionary<string, AudioClip> audioClipsDic = new Dictionary<string, AudioClip>(); //효과음 딕셔너리

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); //여러 씬에서 사용할 것.

        BGMPlayer = GameObject.Find("BGMSoundPlayer").GetComponent<AudioSource>();
        sfxPlayer = GameObject.Find("SFXSoundPlayer").GetComponent<AudioSource>();

        foreach (AudioClip audioclip in sfxAudioClips)
        {
            audioClipsDic.Add(audioclip.name, audioclip);
        }
    }

    // 효과 사운드 재생 : 이름을 필수 매개변수, 볼륨을 선택적 매개변수로 지정
    public void PlaySFXSound(string name, float volume = 1f)
    {
        if (audioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipsDic[name], volume * masterVolumeSFX);
    }

    //BGM 사운드 재생 : 볼륨을 선택적 매개변수로 지정
    public void PlayBGMSound(float volume = 0.5f)
    {
        BGMPlayer.loop = true; //BGM 사운드이므로 루프설정
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
        //현재 씬에 맞는 BGM 재생
    }

}
