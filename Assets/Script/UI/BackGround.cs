using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BackGround : MonoBehaviour
{
    public SpriteAtlas Atlas;

    void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = Atlas.GetSprite("House");
    }

    
    void Update()
    {
        
    }
}
