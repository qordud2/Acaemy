using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text : MonoBehaviour
{
    public UnityEngine.UI.Text myText;
    //public Text MyText;
    // Start is called before the first frame update
    void Start()
    {
        //myText.text = "Hello"; // 안이면 안 밖이면 밖 - 내적과 외적을 통해서
        myText.text = "안";


    }
//using UnityEngine.UI;
//public Text myText;
//myText.GetComponent<Text>().text= "sfdsfsdf";

    // Update is called once per frame
    void Update()
    {
        
    }
}
