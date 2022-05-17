using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatItem : MonoBehaviour
{
    public TMPro.TMP_Text myText = null;

    public void SetText(string str)
    {
        Vector2 size = GetComponent<RectTransform>().sizeDelta; // width와 height
        Vector2 textsize = myText.GetPreferredValues(str);    // str의 전체 사이즈를 전달한다

        int line = (int)(textsize.x / myText.GetComponent<RectTransform>().sizeDelta.x) + 1;
        size.y = (float)line * textsize.y;

        GetComponent<RectTransform>().sizeDelta = size;


        myText.text = str;
    }

    public void Change(int i)
    {
        if (i == 0)
            myText.color = Color.green;
        else if (i == 1)
            myText.color = Color.red;
        else if (i == 2)
            myText.color = Color.blue;
        else
            myText.color = Color.yellow;
    }
}
