using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSlider : MonoBehaviour
{
    public enum STATE
    {
        NORMAL, CHANGE
    }

    STATE myState = STATE.NORMAL;

    Slider healthSlider;
    //float maxHealth = 100;
    public float health { get; private set; }
    float dir = 1f;

    public void ChangeState(STATE s)
    {
        if (myState == s)
            return;
        myState = s;

        switch (myState)
        {
            case STATE.NORMAL:
                dir *= -1;
                break;
            case STATE.CHANGE:
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.NORMAL:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    myState = STATE.CHANGE;
                }
                    break;
            case STATE.CHANGE:
                health = Mathf.Clamp(healthSlider.value + dir * Time.deltaTime * 100f, 0, 100f);
                healthSlider.value = health;
                if(dir == 1f)
                {
                    if (health >= 100f)
                    {
                        ChangeState(STATE.NORMAL);
                    }
                }
                else if(dir == -1f)
                {
                    if (health <= Mathf.Epsilon)
                    {
                        ChangeState(STATE.NORMAL);
                    }
                }
                
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GetComponent<Slider>();
        healthSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
        Debug.Log("health : " + health);
    }
}
