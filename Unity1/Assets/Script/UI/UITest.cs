using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    public GameObject player;
    Rigidbody2D rigid;

    public void OnLeftClick()
    {
        Debug.Log("LeftClick");
        rigid = player.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
        rigid.AddForce(new Vector2(-100.0f, 0));
        
    }

    public void OnRightClick()
    {
        Debug.Log("RightClick");
        rigid = player.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
        rigid.AddForce(new Vector2(100.0f, 0));
    }

    public void OnJumpClick()
    {
        Debug.Log("JumpClick");
        rigid = player.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
        rigid.AddForce(new Vector2(0, 600));
    }
}
