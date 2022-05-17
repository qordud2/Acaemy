using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public enum STATE
    {
        CREATE, PLUS, MIUS
    }

    STATE myState = STATE.CREATE;

    public Slider healthSlider;
    float maxHealth = 100;
    public float health { get; private set; }
    float damage = 20;

    public void ChangeState(STATE s)
    {
        if (myState == s)
            return;
        switch(myState)
        {
            case STATE.CREATE:
                break;
            case STATE.PLUS:
                break;
            case STATE.MIUS:
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.PLUS:
                break;
            case STATE.MIUS:
                break;
        }
    }

    void Init()
    {
        healthSlider = GetComponent<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
        Debug.Log(health);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            health += damage;
            if (health >= 100)
                health = 100;

        }
        healthSlider.value = health;
    }
}
