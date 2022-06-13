using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class wordcount : MonoBehaviour
{

    public TextMeshProUGUI word_count;
    public int max,min;

    public void minuscount()
    {
        int wrdcount=int.Parse(word_count.text);
        if (wrdcount > min)
        {
            wrdcount--;
            word_count.text = wrdcount.ToString();
        }
    }
    public void pluscount()
    {
        int wrdcount = int.Parse(word_count.text);
        if (wrdcount < max)
        {
            wrdcount++;
            word_count.text = wrdcount.ToString();
        }
    }

}
