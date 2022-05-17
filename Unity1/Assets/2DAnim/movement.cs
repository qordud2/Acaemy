using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Animator myAnim;
    public Rigidbody2D rigid;

    //public event MouseEventData MouseDownEvent;


    public string rightname = "Horizontal";
    public string upname = "Vertical";

    public float move { get; private set; }
    public float speed = 3.0f;
    public bool moving { get; private set; }
    public bool jumping { get; private set; }
    public float jumpForce = 300f; 

    void Start()
    {
        myAnim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {      
         myAnim.SetBool("Move", moving);
        if (Input.GetKeyDown(KeyCode.Space))
            jumping = true;


    }

    void FixedUpdate()
    {
        move = Input.GetAxis(rightname);

        

        Move();
        Jump();
        MouseClick();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        float xMove = Input.GetAxis(rightname);
        float yMove = Input.GetAxis(upname);

        if (move > Mathf.Epsilon || move < -Mathf.Epsilon)
        {
            //Vector2 moveDistance = move * transform.right * speed * Time.fixedDeltaTime;
            //rigid.MovePosition((Vector2)transform.position + moveDistance);

            //Vector3 dir = Vector3.right * move;
            //Vector3 pos = transform.position;
            //pos += dir * speed * Time.fixedDeltaTime;
            //rigid.MovePosition(pos);

            moveVelocity = Vector3.right * move;
            Vector3 pos = transform.position;
            //pos += moveVelocity * speed * Time.fixedDeltaTime;
            transform.position += moveVelocity * speed * Time.fixedDeltaTime;
            //rigid.MovePosition(pos);

            //Vector2 getVal = new Vector2(xMove, 0) * speed;
            //rigid.velocity = getVal;
            moving = true;
        }
        else
            moving = false;
    }

    public void Jump()
    {
        if (!jumping)
            return;
        
        rigid.velocity = Vector2.zero;
        rigid.AddForce(new Vector2(0, jumpForce));
        jumping = false;      
    }

    public void MouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Down");
        }
    }
}
