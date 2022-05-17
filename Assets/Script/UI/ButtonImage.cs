using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class ButtonImage : MonoBehaviour
{
    public SpriteAtlas Atlas;
    public string imagename;
    void Awake()
    {
        GetComponent<Image>().sprite = Atlas.GetSprite(imagename);
    }

    void Update()
    {
        
    }
}
