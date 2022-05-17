using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatSystem : MonoBehaviour
{
    public Transform trContents;
    public TMPro.TMP_InputField myTextInput = null;
    public Scrollbar myVerticalScroll = null;
    bool IgnorNextReturn = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //if(!myTextInput.IsActive())
            //    myTextInput.ActivateInputField();

            if (IgnorNextReturn)
            {
                IgnorNextReturn = false;
            }
            else
            {
                myTextInput.ActivateInputField();
            }
            
        }
    }

    public void AddChat(string str)
    {
        if(str.Length > 0)
        {
            GameObject obj = Instantiate(Resources.Load("ChatItem")) as GameObject;
            obj.transform.SetParent(trContents);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one; // 1 1 1
            ChatItem scp = obj.GetComponent<ChatItem>();
            scp.SetText(str);

            myTextInput.text = "";
            myTextInput.ActivateInputField();
            StartCoroutine(SetScrollZero(10.0f));
        }
        else
        {
            IgnorNextReturn = true;
            myTextInput.DeactivateInputField();
        }

        if (string.IsNullOrEmpty(myTextInput.text))
        {
            bool a = true;
        }
    }

    IEnumerator SetScrollZero(float speed)
    {
        // EndofFramge 두번 실행해도 대고 0.1f 해도됨
        // yield return new WaitForEndOfFrame();
        // yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.1f);
        
        while(myVerticalScroll.value > Mathf.Epsilon)
        {
            float delta = Time.smoothDeltaTime * speed;
            myVerticalScroll.value = Mathf.Clamp(myVerticalScroll.value - delta, 0f, 1f);
            yield return null;
        }
        
    }

}
