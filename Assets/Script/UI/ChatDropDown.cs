using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatDropDown : MonoBehaviour
{
    public string[] ChannelTypes = { "일반", "전체", "파티", "채널" };
    TMPro.TMP_Dropdown myChannel;
    public ChatItem ChatItem;

    // Start is called before the first frame update
    void Start()
    {
        myChannel = GetComponent<TMPro.TMP_Dropdown>();
        foreach (string name in ChannelTypes)
        {
            TMPro.TMP_Dropdown.OptionData Channels = new TMPro.TMP_Dropdown.OptionData(name);
            myChannel.options.Add(Channels);
        }

        myChannel.value = 1;
    }

}
