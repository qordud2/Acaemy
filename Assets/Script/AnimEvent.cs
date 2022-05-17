using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public VoidDelVoid Attack = null;
    public UnityAction JumpStart = null;
    public UnityAction JumpEnd = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnAttack()
    {
        Attack?.Invoke();
    }

    void OnJump()
    {
        JumpStart?.Invoke();
    }

    void OnJumpEnd()
    {
        //if (JumpEnd != null) JumpEnd(); 이거랑 아래랑 같은말임
        JumpEnd?.Invoke();
    }
}
