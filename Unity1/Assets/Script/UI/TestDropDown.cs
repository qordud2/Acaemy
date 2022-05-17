using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestDropDown : MonoBehaviour
{
    public string[] ItemTypes = { "Weapon", "Armor", "Potion", "Rune" };
    public TMPro.TMP_Text myText;
    TMPro.TMP_Dropdown myMenu;

    // Start is called before the first frame update
    void Start()
    {
        myMenu = GetComponent<TMPro.TMP_Dropdown>();
        foreach(string name in ItemTypes)
        {
            TMPro.TMP_Dropdown.OptionData item = new TMPro.TMP_Dropdown.OptionData(name);
            myMenu.options.Add(item);
            //myMenu.options.Add(new TMPro.TMP_Dropdown.OptionData(name));  // 이렇게 해도댐
        }
        //TMPro.TMP_Dropdown.OptionData item = new TMPro.TMP_Dropdown.OptionData("Weapon");
        //myMenu.options.Add(item);
        //item = new TMPro.TMP_Dropdown.OptionData("Armor");
        //myMenu.options.Add(item);
        //item = new TMPro.TMP_Dropdown.OptionData("Potion");
        //myMenu.options.Add(item);
        //item = new TMPro.TMP_Dropdown.OptionData("Rune");
        //myMenu.options.Add(item);
        // 입력된 순서로 0 1 2 3인데 value = num 으로 선택할 수 있다
        myMenu.value = 1;
        myText.text = ItemTypes[myMenu.value];
        myMenu.onValueChanged.AddListener(OnChangeMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnChangeMenu(int i)
    {
        myText.text = ItemTypes[i];
    }
}
