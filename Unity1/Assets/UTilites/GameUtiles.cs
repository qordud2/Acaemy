using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtiles : MonoBehaviour
{
    static Canvas _canvas = null;

    public static Canvas CANVAS
    {
        get
        {
            if (_canvas == null)
                _canvas = FindObjectOfType<Canvas>();
            return _canvas;
        }
    }


}
