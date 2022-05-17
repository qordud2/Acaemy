using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public TestUI myTester;
    public Button myButton;

    // Start is called before the first frame update
    void Start()
    {
        // onClick 한거랑 동일한 효과를 얻는다
        myButton.onClick.AddListener(ClosePopup);
        //myButton.onClick.AddListener(
        //    () =>
        //    {
        //        Destroy(gameObject);
        //    }
        //    );

        //myTester.myclose += ClosePopup;
        //myTester.myclose -= ClosePopup;
        //myTester.myclose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePopup()
    {
        
        Destroy(this.gameObject);
    }
}
