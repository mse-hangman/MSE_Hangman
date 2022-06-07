using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Emoji_Manger : MonoBehaviour
{
    public GameObject[] Emoji = new GameObject[4];
    public GameObject nowEmoji;
    public GameObject other_nowEmoji;
    public bool Emojicheck = true;
    public GameObject EmojiPositon;
    public GameObject other_EmojiPositon;

    public IEnumerator MakeEmoji(int index)
    {
        GameObject.Find("network_manager(Clone)").GetComponent<Network_manager>().SendEmojiMessage(index);
        nowEmoji = Instantiate(Emoji[index], EmojiPositon.GetComponent<RectTransform>());
        nowEmoji.transform.parent = this.transform;
        Emojicheck = false;
        yield return new WaitForSeconds(10);
        Emojicheck = true;
        Destroy(nowEmoji);
        yield return 0;
    }
    public void EmojiButton(int index)
    {
        if (Emojicheck)
        {
            StartCoroutine(MakeEmoji(index));
        }
        else
        {

        }
    }
    public IEnumerator OtherPlayerEmoji(int index)
    {
        other_nowEmoji = Instantiate(Emoji[index], other_EmojiPositon.GetComponent<RectTransform>());
        other_nowEmoji.transform.parent = this.transform;
        Emojicheck = false;
        yield return new WaitForSeconds(10);
        Emojicheck = true;
        Destroy(other_nowEmoji);
        yield return 0;
    }
}
